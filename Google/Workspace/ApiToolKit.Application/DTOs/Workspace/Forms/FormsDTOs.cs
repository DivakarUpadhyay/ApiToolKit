using System.ComponentModel.DataAnnotations;

namespace ApiToolKit.Application.DTOs.Workspace.Forms;

public class QuizSettings
{
    public bool IsQuiz { get; set; }
}

public class FormSettingsResponse
{
    public QuizSettings? QuizSettings { get; set; }
}

public class ChoiceQuestion
{
    public string? Type { get; set; }
    public List<object>? Options { get; set; }
    public bool Shuffle { get; set; }
}

public class ScaleQuestion
{
    public int Low { get; set; }
    public int High { get; set; }
    public string? LowLabel { get; set; }
    public string? HighLabel { get; set; }
}

public class Question
{
    public string? QuestionId { get; set; }
    public bool Required { get; set; }
    public object? Grading { get; set; }
    public ChoiceQuestion? ChoiceQuestion { get; set; }
    public object? TextQuestion { get; set; }
    public ScaleQuestion? ScaleQuestion { get; set; }
    public object? DateQuestion { get; set; }
    public object? TimeQuestion { get; set; }
    public object? FileUploadQuestion { get; set; }
    public object? RowQuestion { get; set; }
}

public class QuestionItem
{
    public Question? Question { get; set; }
}

public class FormItemResponse
{
    public string? ItemId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Kind { get; set; }
    public QuestionItem? QuestionItem { get; set; }
}

public class FormInfo
{
    public string? Title { get; set; }
    public string? DocumentTitle { get; set; }
    public string? Description { get; set; }
}

public class FormResponse2
{
    public string? FormId { get; set; }
    public FormInfo? Info { get; set; }
    public FormSettingsResponse? Settings { get; set; }
    public List<FormItemResponse>? Items { get; set; }
    public string? RevisionId { get; set; }
    public string? ResponderUri { get; set; }
}

public class CreateFormRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string? DocumentTitle { get; set; }
}

public class UpdateFormRequest
{
    [Required]
    public string FormId { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsQuiz { get; set; }
}

public class AddItemRequest2
{
    [Required]
    public string FormId { get; set; } = string.Empty;

    [Required]
    public string ItemTitle { get; set; } = string.Empty;
    public string? ItemDescription { get; set; }
    public string ItemType { get; set; } = "SHORT_ANSWER";
    public bool Required { get; set; } = false;
    public List<string>? Choices { get; set; }
    public int ScaleMin { get; set; }
    public int ScaleMax { get; set; }
    public string? LowLabel { get; set; }
    public string? HighLabel { get; set; }
    public bool InsertAtEnd { get; set; } = true;
}

public class TextAnswers
{
    public List<string>? Answers { get; set; }
}

public class GradeInfo
{
    public double Score { get; set; }
    public bool? Correct { get; set; }
}

public class AnswerDto
{
    public string? QuestionId { get; set; }
    public GradeInfo? Grade { get; set; }
    public TextAnswers? TextAnswers { get; set; }
    public object? FileUploadAnswers { get; set; }
    public int? ScaleAnswer { get; set; }
}

public class FormResponseDto
{
    public string? ResponseId { get; set; }
    public string? FormId { get; set; }
    public string? CreateTime { get; set; }
    public string? LastSubmittedTime { get; set; }
    public string? RespondentEmail { get; set; }
    public double? TotalScore { get; set; }
    public Dictionary<string, AnswerDto>? Answers { get; set; }
}

public class ListFormResponsesRequest
{
    [Required]
    public string FormId { get; set; } = string.Empty;
    public int PageSize { get; set; } = 100;
    public string? PageToken { get; set; }
    public string? Filter { get; set; }
}

public class ListFormResponsesResponse
{
    public List<FormResponseDto>? Responses { get; set; }
    public string? NextPageToken { get; set; }
}

public class DeleteItemRequest2
{
    [Required]
    public string FormId { get; set; } = string.Empty;

    [Required]
    public string ItemId { get; set; } = string.Empty;
}

public class GetFormRequest
{
    [Required]
    public string FormId { get; set; } = string.Empty;
}
