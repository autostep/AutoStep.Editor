using AutoStep.Editor.Client.Language;
using AutoStep.Projects;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    /// <summary>
    /// <see cref="CodeWindowReducer"/>
    /// </summary>
    public interface ICodeWindowAction : IAutoStepAction
    {
    }

    /// <summary>
    /// <see cref="ChangeFileEffect"/>
    /// </summary>
    public class ChangeFileAction : ICodeWindowAction
    {
        public ProjectFileState NewFile { get; }

        public ChangeFileAction(ProjectFileState newFile)
        {
            NewFile = newFile;
        }
    }
    
    public class LoadCodeCompleteAction : ICodeWindowAction
    {
        public ProjectFileState File { get; set; }

        public LoadCodeCompleteAction(ProjectFileState file)
        {
            File = file;
        }
    }

    public class CodeChangeAction : ICodeWindowAction
    {
        public Project Project { get; }

        public ProjectFileState File { get; }

        public string Body { get; }

        public CodeChangeAction(Project project, ProjectFileState file, string body)
        {
            Project = project;
            File = file;
            Body = body;
        }
    }

}
