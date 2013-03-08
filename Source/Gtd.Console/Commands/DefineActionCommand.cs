namespace Gtd.Shell.Commands
{
    using System;
    using System.Linq;

    public class DefineActionCommand : IConsoleCommand
    {
        public string[] Key
        {
            get { return new[] { "action", "a" }; }
        }

        public string Usage
        {
            get { return @"action <project-id> <action description>"; }
        }

        public void Execute(ConsoleEnvironment env, string[] args)
        {
            if (args.Length < 2)
            {
                env.Log.Error("You must provide a project and action");
                return;
            }

            var system = env.ConsoleView.Systems[env.Id];
            var matchedProjects = system.Projects
                .Select(x => x.ProjectId)
                .Where(x => x.Id.Matches(args[0]));

            if (matchedProjects.Count() == 0)
            {
                env.Log.Error("Cannot find project {0}", args[0]);
                return;
            }
            if (matchedProjects.Count() > 1)
            {
                env.Log.Error("{0} matches {1} projects", args[0], matchedProjects.Count());
                return;
            }

            var selectedProject = matchedProjects.First();
            var action = string.Join(" ", args.Skip(1));

            env.TrustedSystem.When(new DefineAction(env.Id, Guid.Empty, selectedProject, action));

            env.Log.Info("Action defined");
        }
    }
}
