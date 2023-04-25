﻿using SherpaOnnx.Core.Structs;
using SherpaOnnx.Core.DLL;
using SherpaOnnx.Core.Model;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace SherpaOnnx.Core
{
    /// <summary>
    /// offline recognizer package
    /// Copyright (c)  2023 by manyeyes
    /// </summary>
    public class OfflineRecognizer : OfflineBase
    {
        private readonly ILogger<OfflineRecognizer> _logger;

        public OfflineRecognizer(SherpaOnnxOfflineTransducer transducer,
            string tokensFilePath, string decoding_method = "greedy_search",
            int sample_rate = 16000, int feature_dim = 80,
            int num_threads = 2, bool debug = false)
        {
            Trace.Assert(File.Exists(transducer.decoder_filename)
                && File.Exists(transducer.encoder_filename)
                && File.Exists(transducer.joiner_filename), "Please provide a model");
            Trace.Assert(File.Exists(tokensFilePath), "Please provide a tokens");
            Trace.Assert(num_threads > 0, "num_threads must be greater than 0");

            SherpaOnnxOfflineParaformer paraformer = new SherpaOnnxOfflineParaformer();
            paraformer.model = "";

            SherpaOnnxOfflineNemoEncDecCtc nemo_ctc = new SherpaOnnxOfflineNemoEncDecCtc();
            nemo_ctc.model = "";

            SherpaOnnxOfflineModelConfig model_config = new SherpaOnnxOfflineModelConfig();
            model_config.transducer = transducer;
            model_config.paraformer = paraformer;
            model_config.nemo_ctc = nemo_ctc;
            model_config.num_threads = num_threads;
            model_config.debug = debug;
            model_config.tokens = tokensFilePath;

            SherpaOnnxFeatureConfig feat_config = new SherpaOnnxFeatureConfig();
            feat_config.sample_rate = sample_rate;
            feat_config.feature_dim = feature_dim;

            SherpaOnnxOfflineRecognizerConfig sherpaOnnxOfflineRecognizerConfig;
            sherpaOnnxOfflineRecognizerConfig.decoding_method = decoding_method;
            sherpaOnnxOfflineRecognizerConfig.feat_config = feat_config;
            sherpaOnnxOfflineRecognizerConfig.model_config = model_config;

            _offlineRecognizer =
                DLL.SherpaOnnxSharp.CreateOfflineRecognizer(sherpaOnnxOfflineRecognizerConfig);
            ILoggerFactory loggerFactory = new LoggerFactory();
            _logger = new Logger<OfflineRecognizer>(loggerFactory);
        }

        public OfflineRecognizer(SherpaOnnxOfflineParaformer paraformer,
            string tokensFilePath, string decoding_method = "greedy_search",
            int sample_rate = 16000, int feature_dim = 80,
            int num_threads = 2, bool debug = false)
        {
            Trace.Assert(File.Exists(paraformer.model), "Please provide a model");
            Trace.Assert(File.Exists(tokensFilePath), "Please provide a tokens");
            Trace.Assert(num_threads > 0, "num_threads must be greater than 0");

            SherpaOnnxOfflineTransducer transducer = new SherpaOnnxOfflineTransducer();
            transducer.encoder_filename = "";
            transducer.decoder_filename = "";
            transducer.joiner_filename = "";

            SherpaOnnxOfflineNemoEncDecCtc nemo_ctc = new SherpaOnnxOfflineNemoEncDecCtc();
            nemo_ctc.model = "";

            SherpaOnnxOfflineModelConfig model_config = new SherpaOnnxOfflineModelConfig();
            model_config.transducer = transducer;
            model_config.paraformer = paraformer;
            model_config.nemo_ctc = nemo_ctc;
            model_config.num_threads = num_threads;
            model_config.debug = debug;
            model_config.tokens = tokensFilePath;

            SherpaOnnxFeatureConfig feat_config = new SherpaOnnxFeatureConfig();
            feat_config.sample_rate = sample_rate;
            feat_config.feature_dim = feature_dim;

            SherpaOnnxOfflineRecognizerConfig sherpaOnnxOfflineRecognizerConfig;
            sherpaOnnxOfflineRecognizerConfig.decoding_method = decoding_method;
            sherpaOnnxOfflineRecognizerConfig.feat_config = feat_config;
            sherpaOnnxOfflineRecognizerConfig.model_config = model_config;

            _offlineRecognizer =
                DLL.SherpaOnnxSharp.CreateOfflineRecognizer(sherpaOnnxOfflineRecognizerConfig);
            ILoggerFactory loggerFactory = new LoggerFactory();
            _logger = new Logger<OfflineRecognizer>(loggerFactory);
        }

        public OfflineRecognizer(SherpaOnnxOfflineNemoEncDecCtc nemo_ctc,
            string tokensFilePath, string decoding_method = "greedy_search",
            int sample_rate = 16000, int feature_dim = 80,
            int num_threads = 2, bool debug = false)
        {
            Trace.Assert(File.Exists(nemo_ctc.model), "Please provide a model");
            Trace.Assert(File.Exists(tokensFilePath), "Please provide a tokens");
            Trace.Assert(num_threads > 0, "num_threads must be greater than 0");

            SherpaOnnxOfflineTransducer transducer = new SherpaOnnxOfflineTransducer();
            transducer.encoder_filename = "";
            transducer.decoder_filename = "";
            transducer.joiner_filename = "";

            SherpaOnnxOfflineParaformer paraformer = new SherpaOnnxOfflineParaformer();
            paraformer.model = "";

            SherpaOnnxOfflineModelConfig model_config = new SherpaOnnxOfflineModelConfig();
            model_config.transducer = transducer;
            model_config.paraformer = paraformer;
            model_config.nemo_ctc = nemo_ctc;
            model_config.num_threads = num_threads;
            model_config.debug = debug;
            model_config.tokens = tokensFilePath;

            SherpaOnnxFeatureConfig feat_config = new SherpaOnnxFeatureConfig();
            feat_config.sample_rate = sample_rate;
            feat_config.feature_dim = feature_dim;

            SherpaOnnxOfflineRecognizerConfig sherpaOnnxOfflineRecognizerConfig;
            sherpaOnnxOfflineRecognizerConfig.decoding_method = decoding_method;
            sherpaOnnxOfflineRecognizerConfig.feat_config = feat_config;
            sherpaOnnxOfflineRecognizerConfig.model_config = model_config;

            _offlineRecognizer =
                DLL.SherpaOnnxSharp.CreateOfflineRecognizer(sherpaOnnxOfflineRecognizerConfig);
            ILoggerFactory loggerFactory = new LoggerFactory();
            _logger = new Logger<OfflineRecognizer>(loggerFactory);
        }

        internal OfflineStream CreateOfflineStream()
        {
            SherpaOnnxOfflineStream stream = DLL.SherpaOnnxSharp.CreateOfflineStream(_offlineRecognizer);
            return new OfflineStream(stream);
        }

        internal SherpaOnnxOfflineStream[] CreateOfflineStream(List<float[]> samplesList)
        {
            int batch_size = samplesList.Count;
            SherpaOnnxOfflineStream[] streams = new SherpaOnnxOfflineStream[batch_size];
            List<string> wavFiles = new List<string>();
            for (int i = 0; i < batch_size; i++)
            {
                OfflineStream stream = CreateOfflineStream();
                AcceptWaveform(stream._offlineStream, 16000, samplesList[i]);
                streams[i] = stream._offlineStream;
            }
            return streams;
        }

        public OfflineStream[] CreateOfflineStream(List<string> wavFiles, ref TimeSpan total_duration)
        {
            int batch_size = wavFiles.Count;
            OfflineStream[] streams = new OfflineStream[batch_size];
            for (int i = 0; i < batch_size; i++)
            {
                OfflineStream stream = CreateOfflineStream();
                TimeSpan duration = new TimeSpan(0);
                float[] samples = SherpaOnnx.Core.Utils.AudioHelper.GetTestSamples(wavFiles[i], ref duration);
                total_duration += duration;
                AcceptWaveform(stream._offlineStream, 16000, samples);
                streams[i] = stream;
            }
            return streams;
        }

        internal void AcceptWaveform(SherpaOnnxOfflineStream stream, int sample_rate, float[] samples)
        {
            SherpaOnnxSharp.AcceptWaveform(stream, sample_rate, samples, samples.Length);
        }

        internal IntPtr GetStreamsIntPtr(OfflineStream[] streams)
        {
            int streams_len = streams.Length;
            int size = Marshal.SizeOf(typeof(SherpaOnnxOfflineStream));
            IntPtr streamsIntPtr = Marshal.AllocHGlobal(size * streams_len);
            unsafe
            {
                byte* ptrbds = (byte*)(streamsIntPtr.ToPointer());
                for (int i = 0; i < streams_len; i++, ptrbds += (size))
                {
                    IntPtr streamIntptr = new IntPtr(ptrbds);
                    Marshal.StructureToPtr(streams[i]._offlineStream, streamIntptr, false);
                }

            }
            return streamsIntPtr;
        }
        public void DecodeMultipleOfflineStreams(OfflineStream[] streams)
        {
            IntPtr streamsIntPtr = GetStreamsIntPtr(streams);
            SherpaOnnxSharp.DecodeMultipleOfflineStreams(_offlineRecognizer, streamsIntPtr, streams.Length);
            Marshal.FreeHGlobal(streamsIntPtr);
        }

        internal OfflineRecognizerResultEntity GetResult(SherpaOnnxOfflineStream stream)
        {
            IntPtr result_ip = SherpaOnnxSharp.GetOfflineStreamResult(stream);
            OfflineRecognizerResult offlineRecognizerResult = new OfflineRecognizerResult(result_ip);
#pragma warning disable CS8605 // 取消装箱可能为 null 的值。
            SherpaOnnxOfflineRecognizerResult result =
                (SherpaOnnxOfflineRecognizerResult)Marshal.PtrToStructure(
                    offlineRecognizerResult._offlineRecognizerResult, typeof(SherpaOnnxOfflineRecognizerResult));
#pragma warning restore CS8605 // 取消装箱可能为 null 的值。

#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            string text = Marshal.PtrToStringAnsi(result.text);
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            this._logger.LogInformation("GetResult result.text:{0}", text);
            this._logger.LogInformation("GetResult result.text_len:{0}", result.text_len.ToString());
            OfflineRecognizerResultEntity offlineRecognizerResultEntity =
                new OfflineRecognizerResultEntity();
            offlineRecognizerResultEntity.text = text;
            offlineRecognizerResultEntity.text_len = result.text_len;

            return offlineRecognizerResultEntity;
        }

        public List<OfflineRecognizerResultEntity> GetResults(OfflineStream[] streams)
        {
            List<OfflineRecognizerResultEntity> results = new List<OfflineRecognizerResultEntity>();
            foreach (OfflineStream stream in streams)
            {
                OfflineRecognizerResultEntity offlineRecognizerResultEntity = GetResult(stream._offlineStream);
                results.Add(offlineRecognizerResultEntity);
            }
            return results;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                DLL.SherpaOnnxSharp.DestroyOfflineRecognizer(_offlineRecognizer);
                _offlineRecognizer.impl = IntPtr.Zero;
                this._disposed = true;
                base.Dispose();
            }            
        }
    }
}