using System.Linq;
using Microsoft.EntityFrameworkCore;
using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";
// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();
logger.Info("Program started");
while (true)
{
  Console.WriteLine("Choose an option:");
  Console.WriteLine("1. Display all blogs");
  Console.WriteLine("2. Add blog");
  Console.WriteLine("3. Create post");
  Console.WriteLine("4. Display posts");
  char key = Console.ReadKey().KeyChar;
  Console.WriteLine();
  if (key == 'q')
  {
    break;
  }
  var db = new DataContext();
  switch (key)
  {
    case '1':
      // Display all Blogs from the database
      var query = db.Blogs.OrderBy(b => b.Name);
      Console.WriteLine($"{query.Count()} blogs in database");
      Console.WriteLine("All blogs in the database:");
      foreach (var item in query)
      {
        Console.WriteLine(item.Name);
      }
      break;
    case '2':
      // Create and save a new Blog
      Console.Write("Enter a name for a new Blog: ");
      var name = Console.ReadLine();
      var blog = new Blog { Name = name };
      db.AddBlog(blog);
      logger.Info("Blog added - {name}", name);
      logger.Info("Program ended");
      break;

    case '3':

      // Create and save a new Post from a Blog
      Console.WriteLine("Select Blog you would like to post:");
      foreach (var item in db.Blogs)
      {
        Console.WriteLine($"{item.BlogId}. {item.Name}");
      }
      int id = Convert.ToInt32(Console.ReadLine());
      var blogging = db.Blogs.First(b => b.BlogId == id);
      Console.WriteLine("Enter Post title:");
      string? title = Console.ReadLine();
      Console.WriteLine("Enter Post content:");
      string? content = Console.ReadLine();
      var post = new Post { Title = title, Content = content, Blog = blogging };
      db.AddPost(post);
      break;

    case '4':
      // Display all posts
      Console.WriteLine("Select Blog's Posts to display:");
      Console.WriteLine("0. Posts from all blogs");
      foreach (var item in db.Blogs)
      {
        Console.WriteLine($"{item.BlogId}. Posts from {item.Name}");
      }
      int ids = Convert.ToInt32(Console.ReadLine());
      if (ids == 0)
      {
        int postCount = db.Posts.Count();
        Console.WriteLine($"{postCount} posts returned");
        foreach (var poster in db.Posts)
        {
          Console.WriteLine($"Blog: {poster.Blog}");
          Console.WriteLine($"Title: {poster.Title}");
          Console.WriteLine($"Content: {poster.Content}");


        }
      }
      else
      {
        var bloggings = db.Posts.Where(x => x.BlogId == ids);

        int postCount = bloggings.Count();
        Console.WriteLine($"{postCount} posts returned");
        foreach (var poster in bloggings)
        {
          Console.WriteLine($"Blog: {poster.Blog}");
          Console.WriteLine($"Title: {poster.Title}");
          Console.WriteLine($"Content: {poster.Content}");
        }



      }
      db.SaveChanges();
      break;
  }
}