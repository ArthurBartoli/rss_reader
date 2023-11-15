using Models;

using ConsoleGUI;
using ConsoleGUI.Controls;
using ConsoleGUI.Space;

namespace Views {
    internal class ConsoleView {
        public void DisplayArticle(Article article) {
            Console.WriteLine($"* Titre: {article.Title}");
            Console.WriteLine($"* Content: {article.PubDate}");
            Console.WriteLine($"* Description: {article.Description}");
            Console.WriteLine($"* Link: {article.Link}");
            Console.WriteLine("---------------------\n\n");
        }

        public void InitUI()
        {
            ConsoleManager.Setup();
            ConsoleManager.Resize(new Size(150, 40));
            ConsoleManager.Content = new TextBlock { Text = "Hello world" };
        }
    }
}
