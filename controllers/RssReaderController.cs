using rss_reader.models;
using rss_reader.views;
using rss_reader.toolbox;

namespace rss_reader.controllers
{
    class RssReaderController
    {
        public static void MainMenu()
        {
            try
            {
                while (true) // Main enclosure
                {
                    ConsoleView.TitleScreen();
                    FeedList tmp = new();

                    while (true) // Command enclosure
                    {
                        Console.WriteLine("Please enter a command (such as list !)");
                        var input = Console.ReadLine();
                        List<string> command = input != null ? new List<string>(input.Split(" ")) : new List<string> { "main" };

                        Console.WriteLine("=========================================");
                        switch (command[0])
                        {
                            case "main":
                                Console.Clear();
                                ConsoleView.TitleScreen();
                                break;
                            case "help":
                                RssReaderController.Command_Help(command);
                                goto case "root pattern";
                            case "root pattern":
                                Console.WriteLine();
                                break;
                            case "quit":
                                goto case "exit";
                            case "exit":
                                return;

                            case "load":
                                Dictionary<String, (string, string)> export = RSSDataManagement.ListExports();
                                tmp.ImportList(export[command[1]].Item2);
                                Console.WriteLine("### Here is the list of feeds in this list :");
                                ConsoleView.DisplayFeeds(tmp);
                                goto case "root pattern";

                            case "list":
                                Dictionary<String, (string, string)> exports_list = RSSDataManagement.ListExports();
                                Console.WriteLine("Here are all available exports :");
                                foreach (string exportsKey in exports_list.Keys)
                                {
                                    Console.WriteLine($" {exportsKey}: " + exports_list[exportsKey].Item1);
                                }
                                goto case "root pattern";

                            case "display":
                                RssReaderController.Command_Display(command, tmp);
                                goto case "root pattern";

                            default:
                                string guessed_command = RssReaderController.Command_Guess(command[0]);
                                Console.WriteLine("The command was not understood, please enter another command.");
                                Console.WriteLine($"Did you mean {guessed_command} ?");
                                goto case "root pattern";
                        }

                    }
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while browsing the main menu");
                Console.WriteLine(" !!!!!! " + e.Message);

            }
        }

        // COMMAND METHODS BELOW
        static string Command_Guess(string command)
        {
            try
            {
                List<string> COMMAND_LIST = new()
                        {
                            "list", "exit", "main", "load", "display", "help", "quit"
                        };
                Dictionary<float, string> distances = new();
                foreach (string item in COMMAND_LIST)
                {
                    float distance = LevenshteinDistance.Calculate(command, item);
                    distances[distance] = item;
                    /* FYI
                    Given the commands "list", "exit", "main", "load", "display", "help", "quit" and 
                    an input between 1 and 10 characters, there is a 5.04 x 10^-21 probability of
                    hash collapse.
                    This probability falls with more (diverse) commands/more characters in the input.
                    */
                }
                float min_distance = distances.Keys.Min();
                string res = distances[min_distance];

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error during command guessing assistance");
                Console.WriteLine(" !!!!!! " + e.Message);

                return " !!!!!! " + e.Message;
            }
        }
        static void Command_Help(List<string> command)
        {
            try
            {
                List<string> COMMAND_LIST = new()
                    {
                        "list", "exit", "main", "load", "display", "help", "quit"
                    };
                string SOLUTION_DIR = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader");
                string HELP_DIR = Path.Combine(SOLUTION_DIR, "help");

                if (command.Count == 1)
                {
                    Console.WriteLine("Here is a list of available commands");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("* help [command]--> Lists all available commands or gives a detailed help");
                    Console.WriteLine("* list --> Lists all available exports");
                    Console.WriteLine("* exit --> Just exits the program");
                    Console.WriteLine("* main --> Returns to main menu");
                    Console.WriteLine("* load [export_id] --> Loads a feed list into the program");
                    Console.WriteLine("* display (feeds | feed [feed_id] | article [feed_id.article_id])" +
                        " --> Display feed info or the article of a specific feed");
                    return;
                }
                if (COMMAND_LIST.Contains(command[1]))
                {
                    string path_help_file = Path.Combine(HELP_DIR, command[1] + ".txt");
                    using StreamReader F = new(path_help_file);
                    string content = F.ReadToEnd();
                    Console.WriteLine(content);
                    return;
                }
                else
                {
                    Console.WriteLine("Command was not recognized. Please enter 'help' followed by the command or just 'help'");
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while getting help");
                Console.WriteLine(" !!!!!! " + e.Message);
            }
        }

        static void Command_Display(List<string> command, FeedList tmp)
        {
            try
            {
                var possible_commands = new string[3] { "feed", "feeds", "article" };
                if (command.Count == 1 || !possible_commands.Contains(command[1]))
                {
                    Console.WriteLine("You did not specify what you want to display ! Type 'help' for some help on that.");
                    return;
                }

                Dictionary<string, Feed> feeds = tmp.Feeds!; // The constructor should guarantee that the Feeds exists
                if (feeds is not null && (!feeds.Any()))
                {
                    Console.WriteLine("No feeds have been loaded yet");
                }
                switch (command[1])
                {
                    case "feeds":
                        ConsoleView.DisplayFeeds(tmp);
                        goto default;
                    case "feed":
                        string command_end_feed = string.Join(' ', command.Skip(2));
                        if (command_end_feed is not null && feeds is not null)
                        { 
                            Feed targeted_feed = feeds[command_end_feed];
                            Console.WriteLine("####### " + targeted_feed.Title + " #######");
                            Console.WriteLine(targeted_feed.Description);
                            Console.WriteLine(" ---- " + targeted_feed.Link);
                            Console.WriteLine("* Category : " + targeted_feed.Category);
                            Console.WriteLine("* Last Build Date : " + targeted_feed.LastBuildDate);
                            Console.WriteLine("####### ARTICLES");
                            foreach (string k in targeted_feed.Articles.Keys)
                            {
                                Console.WriteLine($"  * {k}: " + targeted_feed.Articles[k].Title);
                            }
                        }
                        else { Console.WriteLine("You did not select a feed. Please type 'display feeds' and select a feed to display."); }
                        goto default;
                    case "article":
                        string command_end_article = string.Join(' ', command.Skip(2));
                        if (command_end_article == null)
                        {
                            Console.WriteLine("You did not select a feed. Please type 'display feeds' and select a feed to display.");
                            goto default;
                        }
                        String[] target = command_end_article.Split(".");
                        Feed feed = feeds![target[0]]; // I don't get why feeds could be null, I guarantee it's not 2 times
                        Article article = feed.Articles[target[1]];
                        ConsoleView.DisplayArticle(article);
                        goto default;
                    default:
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while displaying RSS feed/article");
                Console.WriteLine(" !!!!!! " + e.Message);
            }
        }
    }
}