using System;
using System.Collections.Generic;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.TokenAttributes;
using WellTool.Extra.Tokenizer;

namespace WellTool.Extra.Tokenizer.Engine.Analysis
{
    /// <summary>
    /// Lucene-analysis分词抽象封装
    /// 项目地址：https://github.com/apache/lucene-solr/tree/master/lucene/analysis
    /// </summary>
    public class AnalysisEngine : TokenizerEngine
    {
        private readonly Analyzer _analyzer;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="analyzer">分析器 Analyzer</param>
        public AnalysisEngine(Analyzer analyzer)
        {
            _analyzer = analyzer;
        }

        /// <summary>
        /// 解析文本进行分词
        /// </summary>
        /// <param name="text">需要分词的文本</param>
        /// <returns>分词结果实现</returns>
        public Result Parse(string text)
        {
            var stream = _analyzer.TokenStream("text", text);
            stream.Reset();
            return new AnalysisResult(stream);
        }
    }
}
