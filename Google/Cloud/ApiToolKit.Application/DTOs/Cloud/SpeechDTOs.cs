using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Cloud.Speech;

public class RecognitionAudio
{
    public string? Content { get; set; } // base64 encoded
    public string? Uri { get; set; }     // gs://
}

public class SpeechContext
{
    public List<string>? Phrases { get; set; }
    public double Boost { get; set; } = 0;
}

public class RecognitionConfig
{
    public string Encoding { get; set; } = "LINEAR16"; // LINEAR16/FLAC/MULAW/AMR/AMR_WB/OGG_OPUS/SPEEX_WITH_HEADER_BYTE/MP3/WEBM_OPUS
    public int SampleRateHertz { get; set; } = 16000;
    public int AudioChannelCount { get; set; } = 1;

    [Required]
    public string LanguageCode { get; set; } = "en-US";

    public int MaxAlternatives { get; set; } = 1;
    public bool ProfanityFilter { get; set; } = false;
    public List<SpeechContext>? SpeechContexts { get; set; }
    public bool EnableWordTimeOffsets { get; set; } = false;
    public bool EnableWordConfidence { get; set; } = false;
    public bool EnableAutomaticPunctuation { get; set; } = true;
    public bool EnableSpeakerDiarization { get; set; } = false;
    public int DiarizationSpeakerCount { get; set; } = 2;
    public string Model { get; set; } = "default"; // default/command_and_search/phone_call/video/medical_conversation/medical_dictation
    public bool UseEnhanced { get; set; } = false;
}

public class RecognizeRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public RecognitionConfig Config { get; set; } = new();

    [Required]
    public RecognitionAudio Audio { get; set; } = new();
}

public class WordInfo
{
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? Word { get; set; }
    public double Confidence { get; set; }
    public int SpeakerTag { get; set; }
}

public class SpeechRecognitionAlternative
{
    public string? Transcript { get; set; }
    public double Confidence { get; set; }
    public List<WordInfo>? Words { get; set; }
}

public class SpeechRecognitionResult
{
    public List<SpeechRecognitionAlternative>? Alternatives { get; set; }
    public int ChannelTag { get; set; }
    public string? ResultEndTime { get; set; }
    public string? LanguageCode { get; set; }
}

public class RecognizeResponse
{
    public List<SpeechRecognitionResult>? Results { get; set; }
    public string? TotalBilledTime { get; set; }
    public long RequestId { get; set; }
}

public class LongRunningRecognizeRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public RecognitionConfig Config { get; set; } = new();

    [Required]
    public RecognitionAudio Audio { get; set; } = new();

    public object? OutputConfig { get; set; }
}

public class GetOperationRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string OperationName { get; set; } = string.Empty;
}

public class StreamingRecognitionConfig
{
    [Required]
    public RecognitionConfig Config { get; set; } = new();

    public bool SingleUtterance { get; set; } = false;
    public bool InterimResults { get; set; } = true;
    public bool EnableVoiceActivityEvents { get; set; } = false;
}
