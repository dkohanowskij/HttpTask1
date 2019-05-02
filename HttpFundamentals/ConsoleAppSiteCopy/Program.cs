using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppSiteCopy.Constraints;
using SiteCopy;
using SiteCopy.Abstract;

namespace ConsoleAppSiteCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (!CommandLine.CommandLineParser.Default.ParseArguments(args, options))
            {
                return;
            }

            DirectoryInfo rootDirectory = new DirectoryInfo(options.OutputDirectoryPath);
            IContentSaver contentSaver = new ContentSaver(rootDirectory);
            ILogger logger = new Logger(options.Verbose);
            List<IConstraint> constraints = GetConstraintsFromOptions(options);
            ISiteDownloader downloader = new SiteDownloader(logger, contentSaver, constraints, options.DeepLevel);

            try
            {
                downloader.LoadFromUrl(options.Url);
            }
            catch (Exception ex)
            {
                logger.Log($"Error during site downloading: {ex.Message}");
            }
        }

        public static List<IConstraint> GetConstraintsFromOptions(Options options)
        {
            List<IConstraint> constraints = new List<IConstraint>();
            if (options.AvailableExtensions != null)
            {
                constraints.Add(new FileTypesConstraint(options.AvailableExtensions.Split(',').Select(e => "." + e)));
            }

            constraints.Add(new DomainTransitionConstraint(options.CrossDomainTransition, new Uri(options.Url)));

            return constraints;
        }
    }
}
