using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("words.txt"); // Путь к файлу
        Random rand = new Random();

        while (true)
        {
            Console.Title = "EnglishLanguageTrainer";
            int index = rand.Next(lines.Length);
            string[] parts = lines[index].Split('-');
            string englishWord = parts[0].Trim();
            string correctTranslation = parts[1].Trim();

            Console.Write($"Переведи '{englishWord}': ");
            string userTranslation = Console.ReadLine().Trim();

            if (userTranslation == correctTranslation)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Отлично! Попробуй другое слово.");
                Console.ResetColor(); // сброс цвета на стандартный
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Неправильно! Правильно будет: '{correctTranslation}'. Попробуй другое слово.");
                Console.ResetColor(); // сброс цвета на стандартный 
                
            }
        }
    }
}