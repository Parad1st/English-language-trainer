using System;

namespace MenuProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Запустить тренажер слов");
            Console.WriteLine("2. Запустить тренажер времён");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    RunCode1();
                    break;
                case 2:
                    RunCode2();
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void RunCode1()
        {
            string filePath = "words.txt"; // Путь к файлу

            // Проверяем, существует ли файл
            if (!File.Exists(filePath))
            {
                // Создаем файл, если его нет
                File.WriteAllLines(filePath, new string[] {
                "//Made by Parad1st - https://github.com/Parad1st/English-language-trainer НЕ УДАЛЯЙТЕ ЭТИ ДВЕ СТРОЧКИ", // Создаются две строчки, позже они игнорируются
                "//Hello-здравствуйте,здравствуй"
            });
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Файл words.txt не найден! Создан ноый файл words.txt.");
                Console.ResetColor(); // сброс цвета на стандартный
            }

            string[] lines = File.ReadAllLines(filePath); // Читаем файл
            Random rand = new Random();

            // Игнорируем первые две строчки
            lines = lines.Skip(2).ToArray();

            while (true)
            {
                Console.Title = "EnglishLanguageTrainer"; // Название окна

                if (lines.Length == 0) // Проверяем, не пустой ли файл (первые 2 строчки не считаются)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Файл не содержит слов с переводами. Проверьте файл words.txt в папке вмете с программой.");
                    Console.ResetColor(); // сброс цвета на стандартный
                    break; // Выходим из цикла, если нет слов
                }

                int index = rand.Next(lines.Length);
                string[] parts = lines[index].Split('-'); // Разделяем знаком тире
                string englishWord = parts[0].Trim();
                string correctTranslationsString = parts[1].Trim();
                string[] translations = correctTranslationsString.Split(',').Select(t => t.Trim()).ToArray(); // Разделяем по запятой и очищаем от пробелов

                Console.Write($"Переведи '{englishWord}': ");
                string userTranslation = Console.ReadLine().Trim();

                bool isCorrect = false;
                foreach (string translation in translations)
                {
                    if (userTranslation == translation)
                    {
                        isCorrect = true;
                        break;
                    }
                }

                if (isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Отлично! Попробуй другое слово.");
                    Console.ResetColor(); // сброс цвета на стандартный
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Неверно. Правильные переводы: {string.Join(", ", translations)}. Попробуй другое слово."); // Выводим все варианты
                    Console.ResetColor(); // сброс цвета на стандартный
                }
            }
        }

        static void RunCode2()
        {
            string filePath = "tenses.txt"; // Путь к файлу

            // Проверяем, существует ли файл
            if (!File.Exists(filePath))
            {
                // Создаем файл, если его нет
                File.WriteAllLines(filePath, new string[] {
                "//Made by Parad1st - https://github.com/Parad1st/English-language-trainer НЕ УДАЛЯЙТЕ ЭТИ ДВЕ СТРОЧКИ",
                "//I ... not ... (to keep) expired pills.-do,keep"
            });
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Файл tenses.txt не найден! Создан новый файл tenses.txt.");
                Console.ResetColor(); // сброс цвета на стандартный
            }

            // Читаем файл, игнорируя первые две строчки
            string[] lines = File.ReadAllLines(filePath).Skip(2).ToArray();

            // Проверка на пустоту:
            if (lines.Length == 0)
            {
                // Eсли он пустой
                File.WriteAllLines(filePath, new string[] {
                "//Made by Parad1st - https://github.com/Parad1st/English-language-trainer НЕ УДАЛЯЙТЕ ЭТИ ДВЕ СТРОЧКИ",
                "//I ... not ... (to keep) expired pills.-do,keep"
            });
                 Console.WriteLine("Файл tenses.txt пустой! ");
                // Перезагрузка списка строк после добавления строки
                lines = File.ReadAllLines(filePath).Skip(2).ToArray();
            }

            Random rand = new Random();

            List<Sentence> sentences = new List<Sentence>();
            foreach (string line in lines)
            {
                string[] parts = line.Split('-');
                if (parts.Length >= 2)
                {
                    sentences.Add(new Sentence(parts[0], parts[parts.Length - 1]));
                }
            }

            Console.WriteLine("Добро пожаловать в тренажер английских времён!");

            while (true)
            {
                int sentenceIndex = rand.Next(sentences.Count);
                Sentence currentSentence = sentences[sentenceIndex];
                Console.WriteLine(currentSentence.GetSentenceWithGaps());
                Console.Write("Введите пропущенные слова (разделяйте их запятой): ");
                string userAnswer = Console.ReadLine();

                List<string> userWords = userAnswer.Split(',').Select(x => x.Trim()).ToList();
                List<string> correctWords = currentSentence.CorrectWords.Split(',').Select(x => x.Trim()).ToList();
                if (userWords.Count != correctWords.Count)
                {
                    for (int i = 0; i < correctWords.Count; i++)
                    {
                        if (i >= userWords.Count)
                        {
                            Console.WriteLine($"Ошибка в слове: ... (правильно: {correctWords[i]})");
                        }
                    }
                }
                else if (userWords.SequenceEqual(correctWords))
                {
                    Console.WriteLine("Правильно!");
                }
                else
                {
                    Console.WriteLine("Неправильно! Правильный ответ: " + string.Join(", ", correctWords));
                    for (int i = 0; i < userWords.Count; i++)
                    {
                        if (i < correctWords.Count && !userWords[i].Equals(correctWords[i]))
                        {
                            Console.WriteLine($"Ошибка в слове: {userWords[i]} (правильно: {correctWords[i]})");
                        }
                        else if (i >= correctWords.Count) // Если в ответе пользователя больше слов, чем в правильном
                        {
                            Console.WriteLine($"Ошибка в слове: {userWords[i]} (лишнее слово)");
                        }
                    }
                }

                Console.WriteLine();
            }
        }
    }

    class Sentence
    {
        public string SentenceText { get; private set; }
        public string CorrectWords { get; private set; }

        public Sentence(string sentenceText, string correctWords)
        {
            this.SentenceText = sentenceText;
            this.CorrectWords = correctWords;
        }
        public string GetSentenceWithGaps()
        {
            string[] words = SentenceText.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Contains("..."))
                {
                    words[i] = ".....";
                }
            }
            return string.Join(" ", words);
        }
    }
}
