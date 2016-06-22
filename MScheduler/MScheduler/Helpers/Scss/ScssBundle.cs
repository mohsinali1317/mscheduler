using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;

namespace MScheduler.Helpers.Scss
{
    public class ScssBundle : StyleBundle
    {
        public ScssBundle(string virtualPath)
            : base(virtualPath)
        {
            Builder = new NullBuilder();
            Transforms.Insert(0, new StyleTransformer());
            Orderer = new NullOrderer();
        }


        public ScssBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath)
        {
            Builder = new NullBuilder();
            Transforms.Insert(0, new StyleTransformer());
            Orderer = new NullOrderer();
        }

    }
}