using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoStep.Execution.Interaction;

namespace AutoStep.Editor.Client.Language
{
    public class DemoMethods
    {
        [InteractionMethod("select")]
        public void Select(MethodContext ctxt, string selector)
        {
        }

        [InteractionMethod("withAttribute")]
        public void WithAttribute(MethodContext ctxt, string attributeName, string attributeValue)
        {
        }

        [InteractionMethod("visible")]
        public void Visible(MethodContext ctxt)
        {
        }

        [InteractionMethod("withText")]
        public void WithText(MethodContext ctxt, string text)
        {  
        }

        [InteractionMethod("getInnerText")]
        public void GetInnerText(MethodContext ctxt)
        {           
        }

        [InteractionMethod("click")]
        public void Click(MethodContext ctxt)
        {
        }

        [InteractionMethod("type")]
        public void Type(MethodContext ctxt, string text)
        {
        }

        [InteractionMethod("assertExists")]
        public void AssertExists(MethodContext ctxt)
        {
        }

        [InteractionMethod("assertText")]
        public void AssertText(MethodContext ctxt, string text)
        {
        }
    }
}
