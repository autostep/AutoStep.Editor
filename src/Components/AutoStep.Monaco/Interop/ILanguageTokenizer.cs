using System;
using System.Collections.Generic;
using System.Text;

namespace AutoStep.Monaco.Interop
{
    public interface ILanguageTokenizer
    {
        int GetInitialState();

        TokenizeResult Tokenize(string line, int state);
    }
}
