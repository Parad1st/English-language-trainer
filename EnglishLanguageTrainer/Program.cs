using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
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
                Console.WriteLine($"Неверно. Правильные переводы: {string.Join(", ", translations)}"); // Выводим все варианты
                Console.ResetColor(); // сброс цвета на стандартный
            }
        }
    }
}