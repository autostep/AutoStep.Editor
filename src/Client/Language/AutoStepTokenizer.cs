using AutoStep.Monaco.Interop;
using AutoStep.Projects;
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
            InitScope("entity.other.attribute-name", LineTokenCategory.Annotation);
            InitScope("string", LineTokenCategory.BoundArgument);
            InitScope("support.variable", LineTokenCategory.Variable);
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

        public AutoStepTokenizer(IProjectCompiler projectCompiler)
        {
            this.projectCompiler = projectCompiler;
        }

        [JSInvokable]
        public int GetInitialState()
        {
            return 0;
        }

        [JSInvokable]
        public TokenizeResult Tokenize(string line, int state)
        {
            Console.WriteLine("Tokenise:" + line);

            try
            {
                // Use the project compiler to tokenise.
                var tokenised = projectCompiler.TokeniseLine(line, (LineTokeniserState)state);

                var tokenArray = tokenised.Tokens.Select(x => new LanguageToken(x.StartPosition, GetScopeText(x.Category, x.SubCategory))).ToArray();

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
