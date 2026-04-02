// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace WellTool.Core.Text;

/// <summary>
/// 字符池<br>
/// 提供常用字符的常量，避免重复创建字符对象
/// </summary>
public static class CharPool
{
    /// <summary>
    /// 字符 ' ' (空格)
    /// </summary>
    public const char Space = ' ';

    /// <summary>
    /// 字符 '\t' (制表符)
    /// </summary>
    public const char Tab = '\t';

    /// <summary>
    /// 字符 '\n' (换行符)
    /// </summary>
    public const char NewLine = '\n';

    /// <summary>
    /// 字符 '\r' (回车符)
    /// </summary>
    public const char CarriageReturn = '\r';

    /// <summary>
    /// 字符 '.' (点)
    /// </summary>
    public const char Dot = '.';

    /// <summary>
    /// 字符 ',' (逗号)
    /// </summary>
    public const char Comma = ',';

    /// <summary>
    /// 字符 ';' (分号)
    /// </summary>
    public const char Semicolon = ';';

    /// <summary>
    /// 字符 ':' (冒号)
    /// </summary>
    public const char Colon = ':';

    /// <summary>
    /// 字符 '!' (感叹号)
    /// </summary>
    public const char Exclamation = '!';

    /// <summary>
    /// 字符 '?' (问号)
    /// </summary>
    public const char Question = '?';

    /// <summary>
    /// 字符 '"' (双引号)
    /// </summary>
    public const char DoubleQuote = '"';

    /// <summary>
    /// 字符 '\'' (单引号)
    /// </summary>
    public const char SingleQuote = '\'';

    /// <summary>
    /// 字符 '`' (反引号)
    /// </summary>
    public const char BackQuote = '`';

    /// <summary>
    /// 字符 '~' (波浪号)
    /// </summary>
    public const char Tilde = '~';

    /// <summary>
    /// 字符 '!' (感叹号)
    /// </summary>
    public const char ExclamationMark = '!';

    /// <summary>
    /// 字符 '@' (at符号)
    /// </summary>
    public const char At = '@';

    /// <summary>
    /// 字符 '#' (井号)
    /// </summary>
    public const char Hash = '#';

    /// <summary>
    /// 字符 '$' (美元符号)
    /// </summary>
    public const char Dollar = '$';

    /// <summary>
    /// 字符 '%' (百分号)
    /// </summary>
    public const char Percent = '%';

    /// <summary>
    /// 字符 '^' ( caret )
    /// </summary>
    public const char Caret = '^';

    /// <summary>
    /// 字符 '&' (和号)
    /// </summary>
    public const char Ampersand = '&';

    /// <summary>
    /// 字符 '*' (星号)
    /// </summary>
    public const char Asterisk = '*';

    /// <summary>
    /// 字符 '(' (左括号)
    /// </summary>
    public const char LeftParenthesis = '(';

    /// <summary>
    /// 字符 ')' (右括号)
    /// </summary>
    public const char RightParenthesis = ')';

    /// <summary>
    /// 字符 '-' (连字符)
    /// </summary>
    public const char Hyphen = '-';

    /// <summary>
    /// 字符 '_' (下划线)
    /// </summary>
    public const char Underscore = '_';

    /// <summary>
    /// 字符 '+' (加号)
    /// </summary>
    public const char Plus = '+';

    /// <summary>
    /// 字符 '=' (等号)
    /// </summary>
    public const char Equals = '=';

    /// <summary>
    /// 字符 '[' (左方括号)
    /// </summary>
    public const char LeftBracket = '[';

    /// <summary>
    /// 字符 ']' (右方括号)
    /// </summary>
    public const char RightBracket = ']';

    /// <summary>
    /// 字符 '{' (左大括号)
    /// </summary>
    public const char LeftBrace = '{';

    /// <summary>
    /// 字符 '}' (右大括号)
    /// </summary>
    public const char RightBrace = '}';

    /// <summary>
    /// 字符 '|' (竖线)
    /// </summary>
    public const char Pipe = '|';

    /// <summary>
    /// 字符 '\' (反斜杠)
    /// </summary>
    public const char Backslash = '\\';

    /// <summary>
    /// 字符 '/' (正斜杠)
    /// </summary>
    public const char Slash = '/';

    /// <summary>
    /// 字符 '<' (小于号)
    /// </summary>
    public const char LessThan = '<';

    /// <summary>
    /// 字符 '>' (大于号)
    /// </summary>
    public const char GreaterThan = '>';

    /// <summary>
    /// 字符 '0' (数字0)
    /// </summary>
    public const char Zero = '0';

    /// <summary>
    /// 字符 '1' (数字1)
    /// </summary>
    public const char One = '1';

    /// <summary>
    /// 字符 '2' (数字2)
    /// </summary>
    public const char Two = '2';

    /// <summary>
    /// 字符 '3' (数字3)
    /// </summary>
    public const char Three = '3';

    /// <summary>
    /// 字符 '4' (数字4)
    /// </summary>
    public const char Four = '4';

    /// <summary>
    /// 字符 '5' (数字5)
    /// </summary>
    public const char Five = '5';

    /// <summary>
    /// 字符 '6' (数字6)
    /// </summary>
    public const char Six = '6';

    /// <summary>
    /// 字符 '7' (数字7)
    /// </summary>
    public const char Seven = '7';

    /// <summary>
    /// 字符 '8' (数字8)
    /// </summary>
    public const char Eight = '8';

    /// <summary>
    /// 字符 '9' (数字9)
    /// </summary>
    public const char Nine = '9';

    /// <summary>
    /// 字符 'a' (小写字母a)
    /// </summary>
    public const char LowerA = 'a';

    /// <summary>
    /// 字符 'b' (小写字母b)
    /// </summary>
    public const char LowerB = 'b';

    /// <summary>
    /// 字符 'c' (小写字母c)
    /// </summary>
    public const char LowerC = 'c';

    /// <summary>
    /// 字符 'd' (小写字母d)
    /// </summary>
    public const char LowerD = 'd';

    /// <summary>
    /// 字符 'e' (小写字母e)
    /// </summary>
    public const char LowerE = 'e';

    /// <summary>
    /// 字符 'f' (小写字母f)
    /// </summary>
    public const char LowerF = 'f';

    /// <summary>
    /// 字符 'g' (小写字母g)
    /// </summary>
    public const char LowerG = 'g';

    /// <summary>
    /// 字符 'h' (小写字母h)
    /// </summary>
    public const char LowerH = 'h';

    /// <summary>
    /// 字符 'i' (小写字母i)
    /// </summary>
    public const char LowerI = 'i';

    /// <summary>
    /// 字符 'j' (小写字母j)
    /// </summary>
    public const char LowerJ = 'j';

    /// <summary>
    /// 字符 'k' (小写字母k)
    /// </summary>
    public const char LowerK = 'k';

    /// <summary>
    /// 字符 'l' (小写字母l)
    /// </summary>
    public const char LowerL = 'l';

    /// <summary>
    /// 字符 'm' (小写字母m)
    /// </summary>
    public const char LowerM = 'm';

    /// <summary>
    /// 字符 'n' (小写字母n)
    /// </summary>
    public const char LowerN = 'n';

    /// <summary>
    /// 字符 'o' (小写字母o)
    /// </summary>
    public const char LowerO = 'o';

    /// <summary>
    /// 字符 'p' (小写字母p)
    /// </summary>
    public const char LowerP = 'p';

    /// <summary>
    /// 字符 'q' (小写字母q)
    /// </summary>
    public const char LowerQ = 'q';

    /// <summary>
    /// 字符 'r' (小写字母r)
    /// </summary>
    public const char LowerR = 'r';

    /// <summary>
    /// 字符 's' (小写字母s)
    /// </summary>
    public const char LowerS = 's';

    /// <summary>
    /// 字符 't' (小写字母t)
    /// </summary>
    public const char LowerT = 't';

    /// <summary>
    /// 字符 'u' (小写字母u)
    /// </summary>
    public const char LowerU = 'u';

    /// <summary>
    /// 字符 'v' (小写字母v)
    /// </summary>
    public const char LowerV = 'v';

    /// <summary>
    /// 字符 'w' (小写字母w)
    /// </summary>
    public const char LowerW = 'w';

    /// <summary>
    /// 字符 'x' (小写字母x)
    /// </summary>
    public const char LowerX = 'x';

    /// <summary>
    /// 字符 'y' (小写字母y)
    /// </summary>
    public const char LowerY = 'y';

    /// <summary>
    /// 字符 'z' (小写字母z)
    /// </summary>
    public const char LowerZ = 'z';

    /// <summary>
    /// 字符 'A' (大写字母A)
    /// </summary>
    public const char UpperA = 'A';

    /// <summary>
    /// 字符 'B' (大写字母B)
    /// </summary>
    public const char UpperB = 'B';

    /// <summary>
    /// 字符 'C' (大写字母C)
    /// </summary>
    public const char UpperC = 'C';

    /// <summary>
    /// 字符 'D' (大写字母D)
    /// </summary>
    public const char UpperD = 'D';

    /// <summary>
    /// 字符 'E' (大写字母E)
    /// </summary>
    public const char UpperE = 'E';

    /// <summary>
    /// 字符 'F' (大写字母F)
    /// </summary>
    public const char UpperF = 'F';

    /// <summary>
    /// 字符 'G' (大写字母G)
    /// </summary>
    public const char UpperG = 'G';

    /// <summary>
    /// 字符 'H' (大写字母H)
    /// </summary>
    public const char UpperH = 'H';

    /// <summary>
    /// 字符 'I' (大写字母I)
    /// </summary>
    public const char UpperI = 'I';

    /// <summary>
    /// 字符 'J' (大写字母J)
    /// </summary>
    public const char UpperJ = 'J';

    /// <summary>
    /// 字符 'K' (大写字母K)
    /// </summary>
    public const char UpperK = 'K';

    /// <summary>
    /// 字符 'L' (大写字母L)
    /// </summary>
    public const char UpperL = 'L';

    /// <summary>
    /// 字符 'M' (大写字母M)
    /// </summary>
    public const char UpperM = 'M';

    /// <summary>
    /// 字符 'N' (大写字母N)
    /// </summary>
    public const char UpperN = 'N';

    /// <summary>
    /// 字符 'O' (大写字母O)
    /// </summary>
    public const char UpperO = 'O';

    /// <summary>
    /// 字符 'P' (大写字母P)
    /// </summary>
    public const char UpperP = 'P';

    /// <summary>
    /// 字符 'Q' (大写字母Q)
    /// </summary>
    public const char UpperQ = 'Q';

    /// <summary>
    /// 字符 'R' (大写字母R)
    /// </summary>
    public const char UpperR = 'R';

    /// <summary>
    /// 字符 'S' (大写字母S)
    /// </summary>
    public const char UpperS = 'S';

    /// <summary>
    /// 字符 'T' (大写字母T)
    /// </summary>
    public const char UpperT = 'T';

    /// <summary>
    /// 字符 'U' (大写字母U)
    /// </summary>
    public const char UpperU = 'U';

    /// <summary>
    /// 字符 'V' (大写字母V)
    /// </summary>
    public const char UpperV = 'V';

    /// <summary>
    /// 字符 'W' (大写字母W)
    /// </summary>
    public const char UpperW = 'W';

    /// <summary>
    /// 字符 'X' (大写字母X)
    /// </summary>
    public const char UpperX = 'X';

    /// <summary>
    /// 字符 'Y' (大写字母Y)
    /// </summary>
    public const char UpperY = 'Y';

    /// <summary>
    /// 字符 'Z' (大写字母Z)
    /// </summary>
    public const char UpperZ = 'Z';
}
