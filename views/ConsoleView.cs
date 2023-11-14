using Models;

namespace Views {
    internal class ConsoleView {
        public void DisplayArticle(Article article) {
            Console.WriteLine($"* Titre: {article.Title}");
            Console.WriteLine($"* Content: {article.Date}");
            Console.WriteLine($"* Description: {article.Description}");
            Console.WriteLine($"* Link: {article.Link}");
            Console.WriteLine("---------------------\n\n");
        }

    }
}
