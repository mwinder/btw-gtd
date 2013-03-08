namespace Gtd.Shell.Commands
{
    using System.Linq;

    class ListProjectsCommand : IConsoleCommand
    {
        public string[] Key { get { return new string[] { "list", "ls" }; } }
        public string Usage
        {
            get
            {
                return @"list
    Return list of all projects available";
            }
        }

        public void Execute(ConsoleEnvironment env, string[] args)
        {
            if (!env.ConsoleView.Systems.ContainsKey(env.Id))
            {
                env.Log.Error("Trusted System not defined");
                return;
            }

            var system = env.ConsoleView.Systems[env.Id];

            if (args.Length == 1)
            {
                var project = system.Projects.First(x => x.ProjectId.Id.Matches(args[0]));
                if (project == null)
                {
                    env.Log.Error("Cannot find project {0}", args[0]);
                    return;
                }

                env.Log.Info("Actions from project {0} ({1} records)", project.Outcome, project.Actions.Count);

                foreach (var entry in project.Actions)
                {
                    env.Log.Info("  {0}  {1, -60}", entry.ActionId.Id.Shorten(), entry.Outcome);
                }
                return;
            }

            var projects = system.Projects;
            env.Log.Info("Projects ({0} records)", projects.Count);

            foreach (var entry in projects)
            {
                var shortId = entry.ProjectId.Id.Shorten();
                env.Log.Info(string.Format("  {0}  {1, -60}", shortId, entry.Outcome));
            }

        }
    }
}