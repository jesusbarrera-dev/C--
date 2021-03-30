using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace C__
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Para terminar el programa ingrese la cadena [ terminar ]\n");
            bool isInvalidToken;
            while (true)
            {
                Console.Write("Ingrese la cadena a evaluar > ");
                var line = Console.ReadLine();
                if(line == "terminar"){
                    break;
                }
                isInvalidToken = true;
                //var lexer = new Lexer(line);
                var lexer = new Program();
                lexer.AnalyzeToken(line, isInvalidToken);
            }

        }

        private void AnalyzeToken(string line, bool isInvalidToken)
        {
            Regex intNumber = new Regex(@"\d");
            Regex az = new Regex(@"\w[a-zA-Z]");
            var regexNumber = intNumber.Match(line);
            var regexWord = az.Match(line);

            switch (line)
            {
                case " ":
                    Console.WriteLine("Cadena vacía");
                    break;
                case "int":
                    Console.WriteLine("Palabra reservada - Tipo de dato " + line);
                    break;
                case "float":
                    Console.WriteLine("Palabra reservada - Tipo de dato " + line);
                    break;
                case "string":
                    Console.WriteLine("Palabra reservada - Tipo de dato " + line);
                    break;
                case "||":
                    Console.WriteLine("Operador lógico - disyunción lógica - " + line);
                    break;
                case "bool":
                    Console.WriteLine("Palabra reservada - Tipo de dato " + line);
                    break;
                case ".":
                    Console.WriteLine("Punto - " + line);
                    break;
                case "true":
                    Console.WriteLine("Palabra reservada - Booleano - " + line);
                    break;
                case "false":
                    Console.WriteLine("Palabra reservada - Booleano - " + line);
                    break;
                case "for":
                    Console.WriteLine("Palabra reservada - Ciclo - " + line);
                    break;
                case "while":
                    Console.WriteLine("Palabra reservada - Ciclo - " + line);
                    break;
                case "if":
                    Console.WriteLine("Palabra reservada - Condicional - " + line);
                    break;
                case "else":
                    Console.WriteLine("Palabra reservada - Condicional - " + line);
                    break;
                case "&&": //----------------------------------------------------------
                    Console.WriteLine("Operador lógico - conjunción lógica - " + line);
                    break;
                case ";":
                    Console.WriteLine("Punto y coma - fin de instrucción - " + line);
                    break;
                case "<":
                    Console.WriteLine("Operador relacional - menor que - " + line);
                    break;
                case ">":
                    Console.WriteLine("Operador relacional - mayor que - " + line);
                    break;
                case ",":
                    Console.WriteLine("Signo de puntuación - coma - " + line);
                    break;
                case "()":
                    Console.WriteLine("Parentesis - " + line);
                    break;
                case "{}":
                    Console.WriteLine("Llaves - " + line);
                    break;
                case "[]":
                    Console.WriteLine("Corchetes - " + line);
                    break;
                case "return":
                    Console.WriteLine("Palabra reservada - " + line);
                    break;
                case "==":
                    Console.WriteLine("Operador relacional - igual a - " + line);
                    break;
                case "void":
                    Console.WriteLine("Palabra reservada - " + line);
                    break;
                case "+":
                    Console.WriteLine("Operador aritmetico - suma - " + line);
                    break;
                case "-":
                    Console.WriteLine("Operador aritmetico - resta - " + line);
                    break;
                case "*":
                    Console.WriteLine("Operador aritmetico - multiplicación - " + line);
                    break;
                case "/":
                    Console.WriteLine("Operador aritmetico - división - " + line);
                    break;
                case "=":
                    Console.WriteLine("Operador de asignación - " + line);
                    break;
                case "++":
                    Console.WriteLine("Operador aritmetico - incremento - " + line);
                    break;
                case "--":
                    Console.WriteLine("Operador aritmetico - decremento - " + line);
                    break;
                default:
                    {
                        if (regexNumber.Success)
                        {
                            Console.WriteLine("Número");
                            isInvalidToken = false;
                        }

                        if (regexWord.Success)
                        {
                            Console.WriteLine("Cadena de caracteres");
                            isInvalidToken = false;
                        }
                        if (isInvalidToken)
                        {
                            Console.WriteLine("ERROR: Token inválido - " + line);
                            isInvalidToken = false;
                        }
                        break;
                    }
            }
        }
    }

    enum SyntaxKind{
        NumberToken,
        WhiteSpaceToken,
        PlusToken,
        MinusToken,
        TimesToken,
        DivisionToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        FloatToken,
        StringToken,
        OrToken,
        boolToken,
        dotToken,
        LoopToken,
        ConditionalToken,
        BadToken,
        EOFToken
    };

    class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
        }

        public override SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }
        public object Value { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }

    class Lexer
    {
        private readonly string _text;
        private int _position;
        private List<string> _diagnostics = new List<string>();

        public Lexer(string text)
        {
            _text = text;
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private char Current
        {
            get
            {
                if (_position >= _text.Length)
                {
                    return '\0';
                }
                return _text[_position];
            }
        }

        private void Next()
        {
            _position++;
        }

        public SyntaxToken nextToken()
        {

            if (_position >= _text.Length)
            {
                return new SyntaxToken(SyntaxKind.EOFToken, _position, "\0", null);
            }

            if (char.IsDigit(Current))
            {
                var start = _position;
                while (char.IsDigit(Current))
                {
                    Next();
                }

                var lenght = _position - start;
                var text = _text.Substring(start, lenght);
                if(!int.TryParse(text, out var value))
                {
                    _diagnostics.Add($"El número {_text} no es Int32 válido.");
                }
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = _position;
                while (char.IsWhiteSpace(Current))
                {
                    Next();
                }

                var lenght = _position - start;
                var text = _text.Substring(start, lenght);
                int.TryParse(text, out var value);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, value);
            }

            if(Current == '+')
            {
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
            }
            else if (Current == '-')
            {
                return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
            }
            else if (Current == '*')
            {
                return new SyntaxToken(SyntaxKind.TimesToken, _position++, "*", null);
            }
            else if (Current == '/')
            {
                return new SyntaxToken(SyntaxKind.DivisionToken, _position++, "/", null);
            }
            else if (Current == '(')
            {
                return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);
            }
            else if (Current == ')')
            {
                return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);
            }
            _diagnostics.Add($"ERROR: entrada de caracter inválido: '{Current}'");
            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);
        }
    }

    class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private List<string> _diagnostics = new List<string>();
        private int _position;

        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();

            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.nextToken();

                if (token.Kind != SyntaxKind.WhiteSpaceToken &&
                    token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }
            } while (token.Kind != SyntaxKind.EOFToken);

            _tokens = tokens.ToArray();
            _diagnostics.AddRange(lexer.Diagnostics);
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private SyntaxToken Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
            {
                return _tokens[_tokens.Length - 1];
            }
            return _tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken(){
            var current = Current;
            _position++;
            return current;
        }

        private SyntaxToken Match(SyntaxKind kind)
        {
            if (Current.Kind == kind)
            {
                return NextToken();
            }
            _diagnostics.Add($"ERROR: token inesperado <{Current.Kind}>, se esperaba <{kind}>");
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseTerm();
        }

        public SyntaxTree Parse()
        {
            var expression = ParseTerm();
            var EOFToken = Match(SyntaxKind.EOFToken);
            return new SyntaxTree(_diagnostics, expression, EOFToken);
        }

        public ExpressionSyntax ParseTerm()
        {
            var left = ParseFactor();

            while (Current.Kind == SyntaxKind.PlusToken ||
                Current.Kind == SyntaxKind.MinusToken)
            {
                var operatorToken = NextToken();
                var right = ParseFactor();
                left = new BinaryExpressionSyntax(left, operatorToken, right)
            }

            return left;
        }

        private ExpressionSyntax ParseFactor()
        {
            var left = ParsePrimaryExpression();

            while (Current.Kind == SyntaxKind.TimesToken ||
                Current.Kind == SyntaxKind.DivisionToken)
            {
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right)
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.OpenParenthesisToken)
            {
                var left = NextToken();
                var expression = ParseExpression();
                var right = Match(SyntaxKind.CloseParenthesisToken);
                return new ParenthesizedExpressionSyntax(left, expression, right);
            }
            var numberToken = Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }

    }

}
