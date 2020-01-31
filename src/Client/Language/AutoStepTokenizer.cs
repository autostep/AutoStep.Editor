using AutoStep.Monaco.Interop;
using AutoStep.Projects;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Language
{
    public class AutoStepTokenizer : ILanguageTokenizer
    {
        private readonly static string[,] scopes = new string[Enum.GetValues(typeof(LineTokenCategory)).Length, Enum.GetValues(typeof(LineTokenSubCategory)).Length];

        static AutoStepTokenizer()
        {
            // Set up our scopes.
            InitScope("comment.line.number-sign", LineTokenCategory.Comment);
            InitScope("keyword", LineTokenCategory.StepTypeKeyword);
            InitScope("keyword", LineTokenCategory.EntryMarker);
            InitScope("entity.name", LineTokenCategory.EntityName);
            InitScope("entity.name.section", LineTokenCategory.EntityName, LineTokenSubCategory.Scenario);
            InitScope("entity.name.section", LineTokenCategory.EntityName, LineTokenSubCategory.ScenarioOutline);
            InitScope("entity.name.type", LineTokenCategory.EntityName, LineTokenSubCategory.Feature);
            InitScope("entity.annotation", LineTokenCategory.Annotation);
            InitScope("entity.annotation.opt", LineTokenCategory.Annotation, LineTokenSubCategory.Option);
            InitScope("entity.annotation.tag", LineTokenCategory.Annotation, LineTokenSubCategory.Tag);
            InitScope("string", LineTokenCategory.BoundArgument);
            InitScope("string.variable", LineTokenCategory.BoundArgument, LineTokenSubCategory.ArgumentVariable);
            InitScope("variable", LineTokenCategory.Variable);
            InitScope("markup.italic", LineTokenCategory.Text, LineTokenSubCategory.Description);
            InitScope("text", LineTokenCategory.Text);
            InitScope("entity.step.text", LineTokenCategory.StepText);
            InitScope("entity.step.text.bound", LineTokenCategory.StepText, LineTokenSubCategory.Bound);
            InitScope("entity.step.text.unbound", LineTokenCategory.StepText, LineTokenSubCategory.Unbound);
            InitScope("table.separator", LineTokenCategory.TableBorder);
        }

        static void InitScope(string scopeText, LineTokenCategory category, LineTokenSubCategory subCategory = LineTokenSubCategory.None)
        {
            scopes[(int)category, (int)subCategory] = scopeText;
        }

        static string GetScopeText(LineTokenCategory category, LineTokenSubCategory subCategory = LineTokenSubCategory.None)
        {
            var scopeText = scopes[(int)category, (int)subCategory];

            if(scopeText is null)
            {
                scopeText = scopes[(int)category, 0];
            }

            if(scopeText is null)
            {
                // Unknown, mark as scope.
                scopeText = "text";
            }

            return scopeText;
        }

        private readonly IProjectCompiler projectCompiler;
        private readonly ILogger logger;

        public AutoStepTokenizer(IProjectCompiler projectCompiler, ILoggerFactory logFactory)
        {
            this.projectCompiler = projectCompiler;
            this.logger = logFactory.CreateLogger<AutoStepTokenizer>();
        }

        [JSInvokable]
        public int GetInitialState()
        {
            return 0;
        }

        [JSInvokable]
        public TokenizeResult Tokenize(string line, int state)
        {
            try
            {
                var castState = (LineTokeniserState)state;

                logger.LogTrace("Tokenise Start in State {0}: {1}", castState, line);
                // Use the project compiler to tokenise.
                var tokenised = projectCompiler.TokeniseLine(line, castState);

                var tokenArray = tokenised.Tokens.Select(x => new LanguageToken(x.StartPosition, GetScopeText(x.Category, x.SubCategory)));

                return new TokenizeResult((int)tokenised.EndState, tokenArray);
            } 
            catch(Exception ex)
            {
                Console.WriteLine("Tokenise Error");
                Console.WriteLine(ex);
            }

            return new TokenizeResult(0, Array.Empty<LanguageToken>());
        }
    }
}
