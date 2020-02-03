using System;
using System.Diagnostics.CodeAnalysis;

namespace AutoStep.Monaco.Interop
{
    /// <summary>
    /// Representation of a language token (for syntax tokenisation). Serialised to JSON, hence the lowercase names.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Mapping to JS Object")]
    public readonly struct LanguageToken : IEquatable<LanguageToken>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageToken"/> struct.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="scopes">The scopes.</param>
        public LanguageToken(int startIndex, string scopes)
        {
            this.startIndex = startIndex;
            this.scopes = scopes;
        }

        /// <summary>
        /// Gets the start position of the token.
        /// </summary>
        public int startIndex { get; }

        /// <summary>
        /// Gets the TextMate scope names.
        /// </summary>
        public string scopes { get; }

        /// <summary>
        /// Equals operator.
        /// </summary>
        /// <param name="left">Left token.</param>
        /// <param name="right">Right token.</param>
        /// <returns>True if the same.</returns>
        public static bool operator ==(LanguageToken left, LanguageToken right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not-equals operator.
        /// </summary>
        /// <param name="left">Left token.</param>
        /// <param name="right">Right token.</param>
        /// <returns>True if different.</returns>
        public static bool operator !=(LanguageToken left, LanguageToken right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is LanguageToken token && Equals(token);
        }

        /// <inheritdoc/>
        public bool Equals(LanguageToken other)
        {
            return startIndex == other.startIndex &&
                   scopes == other.scopes;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(startIndex, scopes);
        }
    }
}
