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
/// 字符串池<br>
/// 提供常用字符串的常量，避免重复创建字符串对象
/// </summary>
public static class StrPool
{
    /// <summary>
    /// 空字符串
    /// </summary>
    public const string Empty = "";

    /// <summary>
    /// 空格字符串
    /// </summary>
    public const string Space = " ";

    /// <summary>
    /// 制表符字符串
    /// </summary>
    public const string Tab = "\t";

    /// <summary>
    /// 换行符字符串
    /// </summary>
    public const string NewLine = "\n";

    /// <summary>
    /// 回车符字符串
    /// </summary>
    public const string CarriageReturn = "\r";

    /// <summary>
    /// 点字符串
    /// </summary>
    public const string Dot = ".";

    /// <summary>
    /// 逗号字符串
    /// </summary>
    public const string Comma = ",";

    /// <summary>
    /// 分号字符串
    /// </summary>
    public const string Semicolon = ";";

    /// <summary>
    /// 冒号字符串
    /// </summary>
    public const string Colon = ":";

    /// <summary>
    /// 感叹号字符串
    /// </summary>
    public const string Exclamation = "!";

    /// <summary>
    /// 问号字符串
    /// </summary>
    public const string Question = "?";

    /// <summary>
    /// 双引号字符串
    /// </summary>
    public const string DoubleQuote = "\"";

    /// <summary>
    /// 单引号字符串
    /// </summary>
    public const string SingleQuote = "'";

    /// <summary>
    /// 反引号字符串
    /// </summary>
    public const string BackQuote = "`";

    /// <summary>
    /// 波浪号字符串
    /// </summary>
    public const string Tilde = "~";

    /// <summary>
    /// 感叹号字符串
    /// </summary>
    public const string ExclamationMark = "!";

    /// <summary>
    /// at符号字符串
    /// </summary>
    public const string At = "@";

    /// <summary>
    /// 井号字符串
    /// </summary>
    public const string Hash = "#";

    /// <summary>
    /// 美元符号字符串
    /// </summary>
    public const string Dollar = "$";

    /// <summary>
    /// 百分号字符串
    /// </summary>
    public const string Percent = "%";

    /// <summary>
    /// caret字符串
    /// </summary>
    public const string Caret = "^";

    /// <summary>
    /// 和号字符串
    /// </summary>
    public const string Ampersand = "&";

    /// <summary>
    /// 星号字符串
    /// </summary>
    public const string Asterisk = "*";

    /// <summary>
    /// 左括号字符串
    /// </summary>
    public const string LeftParenthesis = "(";

    /// <summary>
    /// 右括号字符串
    /// </summary>
    public const string RightParenthesis = ")";

    /// <summary>
    /// 连字符字符串
    /// </summary>
    public const string Hyphen = "-";

    /// <summary>
    /// 下划线字符串
    /// </summary>
    public const string Underscore = "_";

    /// <summary>
    /// 加号字符串
    /// </summary>
    public const string Plus = "+";

    /// <summary>
    /// 等号字符串
    /// </summary>
    public const string Equals = "=";

    /// <summary>
    /// 左方括号字符串
    /// </summary>
    public const string LeftBracket = "[";

    /// <summary>
    /// 右方括号字符串
    /// </summary>
    public const string RightBracket = "]";

    /// <summary>
    /// 左大括号字符串
    /// </summary>
    public const string LeftBrace = "{";

    /// <summary>
    /// 右大括号字符串
    /// </summary>
    public const string RightBrace = "}";

    /// <summary>
    /// 竖线字符串
    /// </summary>
    public const string Pipe = "|";

    /// <summary>
    /// 反斜杠字符串
    /// </summary>
    public const string Backslash = "\\";

    /// <summary>
    /// 正斜杠字符串
    /// </summary>
    public const string Slash = "/";

    /// <summary>
    /// 小于号字符串
    /// </summary>
    public const string LessThan = "<";

    /// <summary>
    /// 大于号字符串
    /// </summary>
    public const string GreaterThan = ">";

    /// <summary>
    /// 数字0字符串
    /// </summary>
    public const string Zero = "0";

    /// <summary>
    /// 数字1字符串
    /// </summary>
    public const string One = "1";

    /// <summary>
    /// 数字2字符串
    /// </summary>
    public const string Two = "2";

    /// <summary>
    /// 数字3字符串
    /// </summary>
    public const string Three = "3";

    /// <summary>
    /// 数字4字符串
    /// </summary>
    public const string Four = "4";

    /// <summary>
    /// 数字5字符串
    /// </summary>
    public const string Five = "5";

    /// <summary>
    /// 数字6字符串
    /// </summary>
    public const string Six = "6";

    /// <summary>
    /// 数字7字符串
    /// </summary>
    public const string Seven = "7";

    /// <summary>
    /// 数字8字符串
    /// </summary>
    public const string Eight = "8";

    /// <summary>
    /// 数字9字符串
    /// </summary>
    public const string Nine = "9";

    /// <summary>
    /// 小写字母a字符串
    /// </summary>
    public const string LowerA = "a";

    /// <summary>
    /// 小写字母b字符串
    /// </summary>
    public const string LowerB = "b";

    /// <summary>
    /// 小写字母c字符串
    /// </summary>
    public const string LowerC = "c";

    /// <summary>
    /// 小写字母d字符串
    /// </summary>
    public const string LowerD = "d";

    /// <summary>
    /// 小写字母e字符串
    /// </summary>
    public const string LowerE = "e";

    /// <summary>
    /// 小写字母f字符串
    /// </summary>
    public const string LowerF = "f";

    /// <summary>
    /// 小写字母g字符串
    /// </summary>
    public const string LowerG = "g";

    /// <summary>
    /// 小写字母h字符串
    /// </summary>
    public const string LowerH = "h";

    /// <summary>
    /// 小写字母i字符串
    /// </summary>
    public const string LowerI = "i";

    /// <summary>
    /// 小写字母j字符串
    /// </summary>
    public const string LowerJ = "j";

    /// <summary>
    /// 小写字母k字符串
    /// </summary>
    public const string LowerK = "k";

    /// <summary>
    /// 小写字母l字符串
    /// </summary>
    public const string LowerL = "l";

    /// <summary>
    /// 小写字母m字符串
    /// </summary>
    public const string LowerM = "m";

    /// <summary>
    /// 小写字母n字符串
    /// </summary>
    public const string LowerN = "n";

    /// <summary>
    /// 小写字母o字符串
    /// </summary>
    public const string LowerO = "o";

    /// <summary>
    /// 小写字母p字符串
    /// </summary>
    public const string LowerP = "p";

    /// <summary>
    /// 小写字母q字符串
    /// </summary>
    public const string LowerQ = "q";

    /// <summary>
    /// 小写字母r字符串
    /// </summary>
    public const string LowerR = "r";

    /// <summary>
    /// 小写字母s字符串
    /// </summary>
    public const string LowerS = "s";

    /// <summary>
    /// 小写字母t字符串
    /// </summary>
    public const string LowerT = "t";

    /// <summary>
    /// 小写字母u字符串
    /// </summary>
    public const string LowerU = "u";

    /// <summary>
    /// 小写字母v字符串
    /// </summary>
    public const string LowerV = "v";

    /// <summary>
    /// 小写字母w字符串
    /// </summary>
    public const string LowerW = "w";

    /// <summary>
    /// 小写字母x字符串
    /// </summary>
    public const string LowerX = "x";

    /// <summary>
    /// 小写字母y字符串
    /// </summary>
    public const string LowerY = "y";

    /// <summary>
    /// 小写字母z字符串
    /// </summary>
    public const string LowerZ = "z";

    /// <summary>
    /// 大写字母A字符串
    /// </summary>
    public const string UpperA = "A";

    /// <summary>
    /// 大写字母B字符串
    /// </summary>
    public const string UpperB = "B";

    /// <summary>
    /// 大写字母C字符串
    /// </summary>
    public const string UpperC = "C";

    /// <summary>
    /// 大写字母D字符串
    /// </summary>
    public const string UpperD = "D";

    /// <summary>
    /// 大写字母E字符串
    /// </summary>
    public const string UpperE = "E";

    /// <summary>
    /// 大写字母F字符串
    /// </summary>
    public const string UpperF = "F";

    /// <summary>
    /// 大写字母G字符串
    /// </summary>
    public const string UpperG = "G";

    /// <summary>
    /// 大写字母H字符串
    /// </summary>
    public const string UpperH = "H";

    /// <summary>
    /// 大写字母I字符串
    /// </summary>
    public const string UpperI = "I";

    /// <summary>
    /// 大写字母J字符串
    /// </summary>
    public const string UpperJ = "J";

    /// <summary>
    /// 大写字母K字符串
    /// </summary>
    public const string UpperK = "K";

    /// <summary>
    /// 大写字母L字符串
    /// </summary>
    public const string UpperL = "L";

    /// <summary>
    /// 大写字母M字符串
    /// </summary>
    public const string UpperM = "M";

    /// <summary>
    /// 大写字母N字符串
    /// </summary>
    public const string UpperN = "N";

    /// <summary>
    /// 大写字母O字符串
    /// </summary>
    public const string UpperO = "O";

    /// <summary>
    /// 大写字母P字符串
    /// </summary>
    public const string UpperP = "P";

    /// <summary>
    /// 大写字母Q字符串
    /// </summary>
    public const string UpperQ = "Q";

    /// <summary>
    /// 大写字母R字符串
    /// </summary>
    public const string UpperR = "R";

    /// <summary>
    /// 大写字母S字符串
    /// </summary>
    public const string UpperS = "S";

    /// <summary>
    /// 大写字母T字符串
    /// </summary>
    public const string UpperT = "T";

    /// <summary>
    /// 大写字母U字符串
    /// </summary>
    public const string UpperU = "U";

    /// <summary>
    /// 大写字母V字符串
    /// </summary>
    public const string UpperV = "V";

    /// <summary>
    /// 大写字母W字符串
    /// </summary>
    public const string UpperW = "W";

    /// <summary>
    /// 大写字母X字符串
    /// </summary>
    public const string UpperX = "X";

    /// <summary>
    /// 大写字母Y字符串
    /// </summary>
    public const string UpperY = "Y";

    /// <summary>
    /// 大写字母Z字符串
    /// </summary>
    public const string UpperZ = "Z";
}
