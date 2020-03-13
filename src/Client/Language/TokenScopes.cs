using System;
using System.Diagnostics.CodeAnalysis;
using AutoStep.Language;

namespace AutoStep.Editor.Client.Language
{
    /// <summary>
    /// Contains the mappings from token category to scope text.
    /// </summary>
    public static class TokenScopes
    {
        [SuppressMessage(
           "Performance",
           "CA1814:Prefer jagged arrays over multidimensional",
           Justification = "Efficient look-up of token scopes prefers multi-dimensional array (even if some space is wasted).")]
        private static readonly string[,] Scopes = new string[
           Enum.GetValues(typeof(LineTokenCategory)).Length,
           Enum.GetValues(typeof(LineTokenSubCategory)).Length];

        static TokenScopes()
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

        /// <summary>
        /// Get the scope text for a given token category and sub-category.
        /// </summary>
        /// <param name="category">The main token category.</param>
        /// <param name="subCategory">The sub-category.</param>
        /// <returns>The applicable scope text.</returns>
        public static string GetScopeText(LineTokenCategory category, LineTokenSubCategory subCategory = LineTokenSubCategory.None)
        {
            var scopeText = Scopes[(int)category, (int)subCategory];

            if (scopeText is null)
            {
                scopeText = Scopes[(int)category, 0];
            }

            if (scopeText is null)
            {
                // Unknown, mark as text.
                scopeText = "text";
            }

            return scopeText;
        }

        private static void InitScope(string scopeText, LineTokenCategory category, LineTokenSubCategory subCategory = LineTokenSubCategory.None)
        {
            Scopes[(int)category, (int)subCategory] = scopeText;
        }
    }
}
