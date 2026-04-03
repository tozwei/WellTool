namespace WellTool.Core.Math;

using WellTool.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathUtil = WellTool.Core.Math.MathUtil;

/// <summary>
/// 数学表达式计算工具类
/// 
/// @author trainliang, looly
/// @since 5.4.3
/// </summary>
public class Calculator
{
    private readonly Stack<string> _postfixStack = new Stack<string>();
    private readonly int[] _operatPriority = new int[] { 0, 3, 2, 1, -1, 1, 0, 2 };

    /// <summary>
    /// 计算表达式的值
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns>计算结果</returns>
    public static double Conversion(string expression)
    {
        return new Calculator().Calculate(expression);
    }

    /// <summary>
    /// 按照给定的表达式计算
    /// </summary>
    /// <param name="expression">要计算的表达式例如:5+12*(3+5)/7</param>
    /// <returns>计算结果</returns>
    public double Calculate(string expression)
    {
        Prepare(Transform(expression));

        Stack<string> resultStack = new Stack<string>();
        List<string> tempList = _postfixStack.ToList();
        tempList.Reverse();
        
        foreach (var item in tempList)
        {
            _postfixStack.Push(item);
        }

        while (_postfixStack.Count > 0)
        {
            string currentOp = _postfixStack.Pop();
            if (!IsOperator(currentOp[0]))
            {
                currentOp = currentOp.Replace("~", "-");
                resultStack.Push(currentOp);
            }
            else
            {
                string secondValue = resultStack.Pop();
                string firstValue = resultStack.Pop();

                firstValue = firstValue.Replace("~", "-");
                secondValue = secondValue.Replace("~", "-");

                decimal tempResult = Calculate(firstValue, secondValue, currentOp[0]);
                resultStack.Push(tempResult.ToString());
            }
        }

        while (resultStack.Count > 1)
        {
            string a = resultStack.Pop();
            string b = resultStack.Pop();
            resultStack.Push(MathUtil.Mul(a, b).ToString());
        }

        return double.Parse(resultStack.Pop());
    }

    /// <summary>
    /// 数据准备阶段将表达式转换成为后缀式栈
    /// </summary>
    /// <param name="expression">表达式</param>
    private void Prepare(string expression)
    {
        expression = StrUtil.CleanBlank(expression);
        if (expression.EndsWith("="))
        {
            expression = expression.Substring(0, expression.Length - 1);
        }

        Stack<char> opStack = new Stack<char>();
        opStack.Push(',');
        char[] arr = expression.ToCharArray();
        int currentIndex = 0;
        int count = 0;
        char peekOp;

        for (int i = 0; i < arr.Length; i++)
        {
            char currentOp = arr[i];
            if (IsOperator(currentOp))
            {
                if (count > 0)
                {
                    _postfixStack.Push(new string(arr, currentIndex, count));
                }
                peekOp = opStack.Peek();
                if (currentOp == ')')
                {
                    while (opStack.Peek() != '(')
                    {
                        _postfixStack.Push(opStack.Pop().ToString());
                    }
                    opStack.Pop();
                }
                else
                {
                    while (currentOp != '(' && peekOp != ',' && Compare(currentOp, peekOp))
                    {
                        _postfixStack.Push(opStack.Pop().ToString());
                        peekOp = opStack.Peek();
                    }
                    opStack.Push(currentOp);
                }
                count = 0;
                currentIndex = i + 1;
            }
            else
            {
                count++;
            }
        }

        if (count > 1 || (count == 1 && currentIndex < arr.Length && !IsOperator(arr[currentIndex])))
        {
            _postfixStack.Push(new string(arr, currentIndex, count));
        }

        while (opStack.Peek() != ',')
        {
            _postfixStack.Push(opStack.Pop().ToString());
        }
    }

    /// <summary>
    /// 判断是否为算术符号
    /// </summary>
    private bool IsOperator(char c)
    {
        return c == '+' || c == '-' || c == '*' || c == '/' || c == '(' || c == ')' || c == '%';
    }

    /// <summary>
    /// 利用ASCII码-40做下标去算术符号优先级
    /// </summary>
    private bool Compare(char cur, char peek)
    {
        int offset = 40;
        if (cur == '%')
        {
            cur = (char)47;
        }
        if (peek == '%')
        {
            peek = (char)47;
        }

        return _operatPriority[peek - offset] >= _operatPriority[cur - offset];
    }

    /// <summary>
    /// 按照给定的算术运算符做计算
    /// </summary>
    private decimal Calculate(string firstValue, string secondValue, char currentOp)
    {
        decimal first = MathUtil.ToDecimal(firstValue);
        decimal second = MathUtil.ToDecimal(secondValue);

        if ((currentOp == '/' || currentOp == '%') && second == 0)
        {
            throw new ArithmeticException($"Division by zero: cannot divide {firstValue} by zero");
        }

        switch (currentOp)
        {
            case '+':
                return first + second;
            case '-':
                return first - second;
            case '*':
                return first * second;
            case '/':
                return first / second;
            case '%':
                return first % second;
            default:
                throw new InvalidOperationException($"Unexpected value: {currentOp}");
        }
    }

    /// <summary>
    /// 将表达式中的一元负号转换为内部标记（~）
    /// </summary>
    private static string Transform(string expression)
    {
        expression = StrUtil.CleanBlank(expression);
        expression = expression.TrimEnd('=');
        char[] arr = expression.ToCharArray();

        StringBuilder output = new StringBuilder(arr.Length);
        for (int i = 0; i < arr.Length; i++)
        {
            char c = arr[i];

            if (c == 'x' || c == 'X')
            {
                output.Append('*');
                continue;
            }

            if (c == '+' || c == '-')
            {
                int outLen = output.Length;
                if (outLen > 0)
                {
                    char prevOut = output[outLen - 1];
                    if (prevOut == 'e' || prevOut == 'E')
                    {
                        if (c == '-')
                        {
                            output.Append('~');
                        }
                        continue;
                    }
                }

                int j = i - 1;
                while (j >= 0 && char.IsWhiteSpace(arr[j])) j--;
                bool unaryContext = (j < 0) || IsPrevCharOperatorOrLeftParen(arr[j]);

                if (unaryContext)
                {
                    int k = i;
                    int minusCount = 0;
                    while (k < arr.Length && (arr[k] == '+' || arr[k] == '-'))
                    {
                        if (arr[k] == '-') minusCount++;
                        k++;
                    }
                    bool netNegative = (minusCount % 2 == 1);
                    if (netNegative)
                    {
                        output.Append('~');
                    }
                    i = k - 1;
                }
                else
                {
                    output.Append(c);
                }
                continue;
            }
            output.Append(c);
        }

        string result = output.ToString();
        char[] resArr = result.ToCharArray();
        if (resArr.Length >= 2 && resArr[0] == '~' && resArr[1] == '(')
        {
            resArr[0] = '-';
            return "0" + new string(resArr);
        }
        return result;
    }

    private static bool IsPrevCharOperatorOrLeftParen(char c)
    {
        return c == '%' || c == '+' || c == '-' || c == '*' || c == '/' || c == '(';
    }
}
