using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";
// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();
logger.Info("Program started");
Console.WriteLine("Choose an option:");
Console.WriteLine("1. Display all blogs");
Console.WriteLine("2. Add blog");
Console.WriteLine("3. Create post");
Console.WriteLine("4. Display posts");
int option = Convert.ToInt32(Console.ReadLine());
var db = new DataContext();
switch (option)
{
  case 1:
    // Display all Blogs from the database
    var query = db.Blogs.OrderBy(b => b.Name);
    Console.WriteLine("All blogs in the database:");
    foreach (var item in query)
    {
      Console.WriteLine(item.Name);
    }
    break;
  case 2:
    // Create and save a new Blog
    Console.Write("Enter a name for a new Blog: ");
    var name = Console.ReadLine();
    var blog = new Blog { Name = name };
    db.AddBlog(blog);
    logger.Info("Blog added - {name}", name);
    logger.Info("Program ended");
    break;
}

