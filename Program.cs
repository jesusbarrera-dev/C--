using System;
using System.Text.RegularExpressions;

namespace C__
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex intNumber = new Regex(@"\d");
            Regex az = new Regex(@"\w[a-zA-Z]");
            bool isInvalidToken = true;

            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();

                var regexNumber = intNumber.Match(line);
                var regexWord = az.Match(line);

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

                //var lexer = new Lexer(line);
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
                    case "bool":
                        Console.WriteLine("Palabra reservada - Tipo de dato " + line);
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
                    default:
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
        FloatToken,
        StringToken,
        OrToken,
        boolToken,
        dotToken,
        LoopToken,
        ConditionalToken
    };

    class SyntaxToken
    {
        public SyntaxToken(SyntaxKind kind, int position, string text)
        {
            Kind = kind;
            Position = position;
            Text = text;
        }

        public SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }
    }

    class Lexer
    {
        private readonly string _text;
        private int _position;

        public Lexer(string text)
        {
            _text = text;
        }

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

        //private void Next()
        //{
        //    _position++;
        //}

        //public SyntaxToken nextToken()
        //{
        //    if (char.IsDigit(Current))
        //    {
        //        var start = _position;
        //        while (char.IsDigit(Current))
        //        {
        //            Next();
        //        }
        //
        //        var lenght = _position - start;
        //        var text = _text.Substring(start, lenght);
        //        return new SyntaxToken(SyntaxKind.NumberToken, start, text);
        //    }
        //}
    }
}
