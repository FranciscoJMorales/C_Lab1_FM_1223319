using System;
using System.Collections.Generic;
using System.Text;

namespace Lab01_FranciscoMorales_1223319
{
    public class Scanner
    {
        private readonly string _algexp = "";
        private int _index = 0;
        private int _state = 0;

        public Scanner(string algexp)
        {
            _algexp = algexp + (char)TokenType.EOF;
            _index = 0;
            _state = 0;
        }

        public Token GetToken()
        {
            Token result = new Token() { Value = 0 };
            bool tokenFound = false;
            while (!tokenFound)
            {
                char peek = _algexp[_index];
                switch (_state)
                {
                    case 0:
                        while (char.IsWhiteSpace(peek))
                        {
                            _index++;
                            peek = _algexp[_index];
                        }
                        switch (peek)
                        {
                            case (char)TokenType.LParen:
                            case (char)TokenType.RParen:
                            case (char)TokenType.Plus:
                            case (char)TokenType.Minus:
                            case (char)TokenType.Mult:
                            case (char)TokenType.Div:
                            case (char)TokenType.EOF:
                                tokenFound = true;
                                result.Tag = (TokenType)peek;
                                break;
                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                _state++;
                                result.Tag = TokenType.TInt;
                                result.Value = Convert.ToInt32(Convert.ToString(peek));
                                break;
                            default:
                                throw new Exception($"Error léxico en la posicion: {_index}");
                        }
                        break;
                    case 1:
                        switch (peek)
                        {
                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                result.Value *= 10;
                                result.Value += Convert.ToInt32(Convert.ToString(peek));
                                break;
                            default:
                                tokenFound = true;
                                _index--;
                                break;
                        }
                        break;
                    default:
                        break;
                }
                _index++;
            }
            _state = 0;
            return result;
        }

        public int GetLastIndex()
        {
            return _index - 1;
        }
    }
}
