using System.Text;
using System.Collections.Generic;

namespace WellTool.DB.Sql
{
    /// <summary>
    /// SQL格式化器 from Hibernate
    /// </summary>
    public static class SqlFormatter
    {
        private static readonly HashSet<string> BeginClauses = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "left", "right", "inner", "outer", "group", "order"
        };

        private static readonly HashSet<string> EndClauses = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "where", "set", "having", "join", "from", "by", "into", "union"
        };

        private static readonly HashSet<string> LogicalKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "and", "or", "when", "else", "end"
        };

        private static readonly HashSet<string> Quantifiers = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "in", "all", "exists", "some", "any"
        };

        private static readonly HashSet<string> Dml = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "insert", "update", "delete"
        };

        private static readonly HashSet<string> MiscKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "select", "on"
        };

        private const string IndentString = "    ";
        private const string Initial = "\n    ";

        /// <summary>
        /// 格式化SQL语句
        /// </summary>
        /// <param name="source">原始SQL</param>
        /// <returns>格式化后的SQL</returns>
        public static string Format(string source)
        {
            return new FormatProcess(source).Perform().Trim();
        }

        /// <summary>
        /// 格式化处理内部类
        /// </summary>
        private class FormatProcess
        {
            private bool _beginLine = true;
            private bool _afterBeginBeforeEnd = false;
            private bool _afterByOrSetOrFromOrSelect = false;
            private bool _afterOn = false;
            private bool _afterBetween = false;
            private bool _afterInsert = false;
            private int _inFunction = 0;
            private int _parensSinceSelect = 0;
            private readonly LinkedList<int> _parenCounts = new LinkedList<int>();
            private readonly LinkedList<bool> _afterByOrFromOrSelects = new LinkedList<bool>();

            private int _indent = 1;

            private readonly StringBuilder _result = new StringBuilder();
            private string _lastToken = string.Empty;
            private string _token = string.Empty;
            private string _lcToken = string.Empty;

            public FormatProcess(string sql)
            {
                // 使用简单的方法分割SQL，避免正则表达式转义问题
                var tokens = new List<string>();
                var currentToken = new StringBuilder();
                
                foreach (var c in sql)
                {
                    if (char.IsWhiteSpace(c) || ",()[]+-*/=<>!&|;:'\"`.\r\n".Contains(c))
                    {
                        if (currentToken.Length > 0)
                        {
                            tokens.Add(currentToken.ToString());
                            currentToken.Clear();
                        }
                        if (!char.IsWhiteSpace(c))
                        {
                            tokens.Add(c.ToString());
                        }
                    }
                    else
                    {
                        currentToken.Append(c);
                    }
                }
                
                if (currentToken.Length > 0)
                {
                    tokens.Add(currentToken.ToString());
                }
                
                Tokens = tokens;
            }

            private List<string> Tokens { get; }

            private int TokenIndex { get; set; }

            public string Perform()
            {
                _result.Append(Initial);
                TokenIndex = 0;

                while (TokenIndex < Tokens.Count)
                {
                    _token = Tokens[TokenIndex];
                    _lcToken = _token.ToLower();

                    if ("'".Equals(_token))
                    {
                        var sb = new StringBuilder();
                        sb.Append(_token);
                        while ((TokenIndex + 1) < Tokens.Count)
                        {
                            TokenIndex++;
                            var t = Tokens[TokenIndex];
                            sb.Append(t);
                            if ("'".Equals(t))
                                break;
                        }
                        _token = sb.ToString();
                    }
                    else if ("\"+".Equals(_token))
                    {
                        var sb = new StringBuilder();
                        sb.Append(_token);
                        while ((TokenIndex + 1) < Tokens.Count)
                        {
                            TokenIndex++;
                            var t = Tokens[TokenIndex];
                            sb.Append(t);
                            if ("\"+".Equals(t))
                                break;
                        }
                        _token = sb.ToString();
                    }
                    else if ("`".Equals(_token))
                    {
                        var sb = new StringBuilder();
                        sb.Append(_token);
                        while ((TokenIndex + 1) < Tokens.Count)
                        {
                            TokenIndex++;
                            var t = Tokens[TokenIndex];
                            sb.Append(t);
                            if ("`".Equals(t))
                                break;
                        }
                        _token = sb.ToString();
                    }

                    if (_afterByOrSetOrFromOrSelect && ",".Equals(_token))
                    {
                        CommaAfterByOrFromOrSelect();
                    }
                    else if (_afterOn && ",".Equals(_token))
                    {
                        CommaAfterOn();
                    }
                    else if ("(".Equals(_token))
                    {
                        OpenParen();
                    }
                    else if (")".Equals(_token))
                    {
                        CloseParen();
                    }
                    else if (BeginClauses.Contains(_lcToken))
                    {
                        BeginNewClause();
                    }
                    else if (EndClauses.Contains(_lcToken))
                    {
                        EndNewClause();
                    }
                    else if ("select".Equals(_lcToken))
                    {
                        Select();
                    }
                    else if (Dml.Contains(_lcToken))
                    {
                        UpdateOrInsertOrDelete();
                    }
                    else if ("values".Equals(_lcToken))
                    {
                        Values();
                    }
                    else if ("on".Equals(_lcToken))
                    {
                        On();
                    }
                    else if (_afterBetween && "and".Equals(_lcToken))
                    {
                        Misc();
                        _afterBetween = false;
                    }
                    else if (LogicalKeywords.Contains(_lcToken))
                    {
                        Logical();
                    }
                    else if (IsWhitespace(_token))
                    {
                        White();
                    }
                    else
                    {
                        Misc();
                    }

                    if (!IsWhitespace(_token))
                    {
                        _lastToken = _lcToken;
                    }

                    // 增加TokenIndex，避免无限循环
                    TokenIndex++;
                }

                return _result.ToString();
            }

            private void CommaAfterOn()
            {
                Out();
                _indent -= 1;
                Newline();
                _afterOn = false;
                _afterByOrSetOrFromOrSelect = true;
            }

            private void CommaAfterByOrFromOrSelect()
            {
                Out();
                Newline();
            }

            private void Logical()
            {
                if ("end".Equals(_lcToken))
                {
                    _indent -= 1;
                }
                Newline();
                Out();
                _beginLine = false;
            }

            private void On()
            {
                _indent += 1;
                _afterOn = true;
                Newline();
                Out();
                _beginLine = false;
            }

            private void Misc()
            {
                Out();
                if ("between".Equals(_lcToken))
                {
                    _afterBetween = true;
                }
                if (_afterInsert)
                {
                    Newline();
                    _afterInsert = false;
                }
                else
                {
                    _beginLine = false;
                    if ("case".Equals(_lcToken))
                    {
                        _indent += 1;
                    }
                }
            }

            private void White()
            {
                if (!_beginLine)
                {
                    _result.Append(' ');
                }
            }

            private void UpdateOrInsertOrDelete()
            {
                Out();
                _indent += 1;
                _beginLine = false;
                if ("update".Equals(_lcToken))
                {
                    Newline();
                }
                if ("insert".Equals(_lcToken))
                {
                    _afterInsert = true;
                }
            }

            private void Select()
            {
                Out();
                _indent += 1;
                Newline();
                _parenCounts.AddLast(_parensSinceSelect);
                _afterByOrFromOrSelects.AddLast(_afterByOrSetOrFromOrSelect);
                _parensSinceSelect = 0;
                _afterByOrSetOrFromOrSelect = true;
            }

            private void Out()
            {
                _result.Append(_token);
            }

            private void EndNewClause()
            {
                if (!_afterBeginBeforeEnd)
                {
                    _indent -= 1;
                    if (_afterOn)
                    {
                        _indent -= 1;
                        _afterOn = false;
                    }
                    Newline();
                }
                Out();
                if (!"union".Equals(_lcToken))
                {
                    _indent += 1;
                }
                Newline();
                _afterBeginBeforeEnd = false;
                _afterByOrSetOrFromOrSelect = ("by".Equals(_lcToken) || "set".Equals(_lcToken) || "from".Equals(_lcToken));
            }

            private void BeginNewClause()
            {
                if (!_afterBeginBeforeEnd)
                {
                    if (_afterOn)
                    {
                        _indent -= 1;
                        _afterOn = false;
                    }
                    _indent -= 1;
                    Newline();
                }
                Out();
                _beginLine = false;
                _afterBeginBeforeEnd = true;
            }

            private void Values()
            {
                _indent -= 1;
                Newline();
                Out();
                _indent += 1;
                Newline();
            }

            private void CloseParen()
            {
                _parensSinceSelect -= 1;
                if (_parensSinceSelect < 0)
                {
                    _indent -= 1;
                    _parensSinceSelect = _parenCounts.Last();
                    _parenCounts.RemoveLast();
                    _afterByOrSetOrFromOrSelect = _afterByOrFromOrSelects.Last();
                    _afterByOrFromOrSelects.RemoveLast();
                }
                if (_inFunction > 0)
                {
                    _inFunction -= 1;
                }
                else
                {
                    if (!_afterByOrSetOrFromOrSelect)
                    {
                        _indent -= 1;
                        Newline();
                    }
                }
                Out();
                _beginLine = false;
            }

            private void OpenParen()
            {
                if (IsFunctionName(_lastToken) || _inFunction > 0)
                {
                    _inFunction += 1;
                }
                _beginLine = false;
                if (_inFunction > 0)
                {
                    Out();
                }
                else
                {
                    Out();
                    if (!_afterByOrSetOrFromOrSelect)
                    {
                        _indent += 1;
                        Newline();
                        _beginLine = true;
                    }
                }
                _parensSinceSelect += 1;
            }

            private static bool IsFunctionName(string tok)
            {
                if (string.IsNullOrEmpty(tok))
                {
                    return true;
                }
                var begin = tok[0];
                var isIdentifier = char.IsLetter(begin) || '"' == begin;
                return isIdentifier && !LogicalKeywords.Contains(tok) && !EndClauses.Contains(tok) 
                    && !Quantifiers.Contains(tok) && !Dml.Contains(tok) && !MiscKeywords.Contains(tok);
            }

            private static bool IsWhitespace(string token)
            {
                return " \n\r\f\t".Contains(token);
            }

            private void Newline()
            {
                _result.Append('\n');
                for (int i = 0; i < _indent; i++)
                {
                    _result.Append(IndentString);
                }
                _beginLine = true;
            }
        }
    }
}
