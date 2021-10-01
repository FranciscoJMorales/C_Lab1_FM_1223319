using System;
using System.Collections.Generic;
using System.Text;

namespace Lab01_FranciscoMorales_1223319
{
    public class Parser
    {
        Scanner _scanner;
        Token _token;

        public double Parse(string regexp)
        {
            _scanner = new Scanner(regexp + (char)TokenType.EOF);
            _token = _scanner.GetToken();
            double result = 0;
            switch (_token.Tag)
            {
                case TokenType.Minus:
                case TokenType.TInt:
                case TokenType.LParen:
                    result = E();
                    break;
                default:
                    break;
            }
            Match(TokenType.EOF);
            return Math.Round(result, 2);
        }

        private double E()
        {
            switch (_token.Tag)
            {
                case TokenType.Minus:
                case TokenType.TInt:
                case TokenType.LParen:
                    return T() + EP();
                default:
                    throw new Exception($"Error de sintaxis en la posicion: { _scanner.GetLastIndex() }");
            }
        }

        private double EP()
        {
            switch (_token.Tag)
            {
                case TokenType.Plus:
                    Match(TokenType.Plus);
                    return T() + EP();
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    return -1 * T() + EP();
                default:
                    return 0;
            }
        }

        private double T()
        {
            switch (_token.Tag)
            {
                case TokenType.Minus:
                case TokenType.TInt:
                case TokenType.LParen:
                    return F() * TP();
                default:
                    throw new Exception($"Error de sintaxis en la posicion: { _scanner.GetLastIndex() }");
            }
        }

        private double TP()
        {
            switch (_token.Tag)
            {
                case TokenType.Mult:
                    Match(TokenType.Mult);
                    return F() * TP();
                case TokenType.Div:
                    Match(TokenType.Div);
                    return (1 / F()) * TP();
                default:
                    return 1;
            }
        }

        private double F()
        {
            switch (_token.Tag)
            {
                case TokenType.Minus:
                    Match(TokenType.Minus);
                    return -1 * M();
                case TokenType.TInt:
                case TokenType.LParen:
                    return M();
                default:
                    throw new Exception($"Error de sintaxis en la posicion: { _scanner.GetLastIndex() }");
            }
        }
        private double M()
        {
            double aux;
            switch (_token.Tag)
            {
                case TokenType.TInt:
                    aux = _token.Value;
                    Match(TokenType.TInt);
                    return aux;
                case TokenType.LParen:
                    Match(TokenType.LParen);
                    aux = E();
                    Match(TokenType.RParen);
                    return aux;
                default:
                    throw new Exception($"Error de sintaxis en la posicion: { _scanner.GetLastIndex() }");
            }
        }

        private void Match(TokenType tag)
        {
            if (_token.Tag == tag)
            {
                _token = _scanner.GetToken();
            }
            else
            {
                throw new Exception($"Error de sintaxis en la posicion: { _scanner.GetLastIndex() }");
            }
        }
    }
}
