using System;
using System.IO;
using Lucene.Net.Analysis.TokenAttributes;
using WellTool.Extra.Tokenizer;

namespace WellTool.Extra.Tokenizer.Engine.Analysis
{
    /// <summary>
    /// Lucene-analysis分词抽象结果封装
    /// 项目地址：https://github.com/apache/lucene-solr/tree/master/lucene/analysis
    /// </summary>
    public class AnalysisResult : AbstractResult
    {
        private readonly Lucene.Net.Analysis.TokenStream _stream;
        private readonly ICharTermAttribute _termAtt;
        private readonly IOffsetAttribute _offsetAtt;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="stream">分词结果</param>
        public AnalysisResult(Lucene.Net.Analysis.TokenStream stream)
        {
            _stream = stream;
            _termAtt = stream.GetAttribute<ICharTermAttribute>();
            _offsetAtt = stream.GetAttribute<IOffsetAttribute>();
        }

        /// <summary>
        /// 获取下一个单词
        /// </summary>
        /// <returns>下一个单词或null</returns>
        protected override WellTool.Extra.Tokenizer.Word NextWord()
        {
            if (_stream.IncrementToken())
            {
                return new AnalysisWord(_termAtt, _offsetAtt);
            }
            return null;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}
