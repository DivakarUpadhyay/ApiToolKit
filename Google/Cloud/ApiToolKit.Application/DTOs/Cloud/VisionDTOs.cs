using System.ComponentModel.DataAnnotations;
using ApiToolKit.Application.DTOs.Cloud.Shared;

namespace ApiToolKit.Application.DTOs.Cloud.Vision;

public class ImageSource
{
    public string? Content { get; set; }
    public string? GcsUri { get; set; }
    public string? ImageUri { get; set; }
}

public class Feature
{
    [Required]
    public string Type { get; set; } = string.Empty; // LABEL_DETECTION/TEXT_DETECTION/DOCUMENT_TEXT_DETECTION/FACE_DETECTION/LANDMARK_DETECTION/LOGO_DETECTION/SAFE_SEARCH_DETECTION/IMAGE_PROPERTIES/OBJECT_LOCALIZATION/PRODUCT_SEARCH/WEB_DETECTION/CROP_HINTS
    public int MaxResults { get; set; } = 10;
}

public class LatLongRect
{
    public double MinLatLng { get; set; }
    public double MaxLatLng { get; set; }
}

public class CropHintsParams
{
    public List<double>? AspectRatios { get; set; }
}

public class ProductSearchParams
{
    public string? ProductSet { get; set; }
    public List<string>? ProductCategories { get; set; }
    public string? Filter { get; set; }
}

public class TextDetectionParams
{
    public bool EnableTextDetectionConfidenceScore { get; set; }
}

public class ImageContext
{
    public List<string>? LanguageHints { get; set; }
    public LatLongRect? LatLongRect { get; set; }
    public CropHintsParams? CropHintsParams { get; set; }
    public ProductSearchParams? ProductSearchParams { get; set; }
    public TextDetectionParams? TextDetectionParams { get; set; }
}

public class AnnotateImageRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public ImageSource Image { get; set; } = new();

    [Required]
    public List<Feature> Features { get; set; } = new();

    public ImageContext? ImageContext { get; set; }
}

public class BoundingPoly
{
    public List<Vertex>? Vertices { get; set; }
    public List<NormalizedVertex>? NormalizedVertices { get; set; }
}

public class Vertex
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class NormalizedVertex
{
    public double X { get; set; }
    public double Y { get; set; }
}

public class LocationInfo
{
    public LatLng? LatLng { get; set; }
}

public class LatLng
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class Property
{
    public string? Name { get; set; }
    public string? Value { get; set; }
    public ulong Uint64Value { get; set; }
}

public class EntityAnnotation
{
    public string? Mid { get; set; }
    public string? Locale { get; set; }
    public string? Description { get; set; }
    public double Score { get; set; }
    public double Confidence { get; set; }
    public double Topicality { get; set; }
    public BoundingPoly? BoundingPoly { get; set; }
    public List<LocationInfo>? Locations { get; set; }
    public List<Property>? Properties { get; set; }
}

public class Landmark
{
    public string? Type { get; set; }
    public Position? Position { get; set; }
}

public class Position
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
}

public class FaceAnnotation
{
    public BoundingPoly? BoundingPoly { get; set; }
    public BoundingPoly? FdBoundingPoly { get; set; }
    public List<Landmark>? Landmarks { get; set; }
    public double RollAngle { get; set; }
    public double PanAngle { get; set; }
    public double TiltAngle { get; set; }
    public double DetectionConfidence { get; set; }
    public double LandmarkingConfidence { get; set; }
    public string? JoyLikelihood { get; set; }
    public string? SorrowLikelihood { get; set; }
    public string? AngerLikelihood { get; set; }
    public string? SurpriseLikelihood { get; set; }
    public string? UnderExposedLikelihood { get; set; }
    public string? BlurredLikelihood { get; set; }
    public string? HeadwearLikelihood { get; set; }
}

public class Page
{
    public string? Property { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public List<object>? Blocks { get; set; }
}

public class TextAnnotation
{
    public string? Text { get; set; }
    public List<Page>? Pages { get; set; }
}

public class SafeSearchAnnotation
{
    public string? Adult { get; set; }
    public string? Spoof { get; set; }
    public string? Medical { get; set; }
    public string? Violence { get; set; }
    public string? Racy { get; set; }
}

public class WebEntity
{
    public string? EntityId { get; set; }
    public double Score { get; set; }
    public string? Description { get; set; }
}

public class WebImage
{
    public string? Url { get; set; }
    public double Score { get; set; }
}

public class WebPage
{
    public string? Url { get; set; }
    public double Score { get; set; }
    public string? PageTitle { get; set; }
}

public class WebLabel
{
    public string? Label { get; set; }
    public string? LanguageCode { get; set; }
}

public class WebDetection
{
    public List<WebEntity>? WebEntities { get; set; }
    public List<WebImage>? FullMatchingImages { get; set; }
    public List<WebImage>? PartialMatchingImages { get; set; }
    public List<WebPage>? PagesWithMatchingImages { get; set; }
    public List<WebImage>? VisuallySimilarImages { get; set; }
    public List<WebLabel>? BestGuessLabels { get; set; }
}

public class LocalizedObjectAnnotation
{
    public string? Mid { get; set; }
    public string? Name { get; set; }
    public double Score { get; set; }
    public BoundingPoly? BoundingPoly { get; set; }
}

public class AnnotationError
{
    public int Code { get; set; }
    public string? Message { get; set; }
}

public class ImageAnnotationResponse
{
    public List<EntityAnnotation>? LabelAnnotations { get; set; }
    public List<FaceAnnotation>? FaceAnnotations { get; set; }
    public List<EntityAnnotation>? LandmarkAnnotations { get; set; }
    public List<EntityAnnotation>? LogoAnnotations { get; set; }
    public List<EntityAnnotation>? TextAnnotations { get; set; }
    public TextAnnotation? FullTextAnnotation { get; set; }
    public SafeSearchAnnotation? SafeSearchAnnotation { get; set; }
    public object? ImagePropertiesAnnotation { get; set; }
    public object? CropHintsAnnotation { get; set; }
    public WebDetection? WebDetection { get; set; }
    public List<LocalizedObjectAnnotation>? LocalizedObjectAnnotations { get; set; }
    public AnnotationError? Error { get; set; }
}

public class BatchAnnotateImagesRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public List<AnnotateImageRequest> Requests { get; set; } = new();
}

public class BatchAnnotateImagesResponse
{
    public List<ImageAnnotationResponse>? Responses { get; set; }
}

public class GcsSource
{
    public string Uri { get; set; } = string.Empty;
}

public class GcsDestination
{
    public string Uri { get; set; } = string.Empty;
}

public class InputConfig
{
    public GcsSource? GcsSource { get; set; }
    public string MimeType { get; set; } = string.Empty;
}

public class OutputConfig
{
    public GcsDestination? GcsDestination { get; set; }
    public int BatchSize { get; set; }
}

public class AsyncAnnotateFileRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    public InputConfig? InputConfig { get; set; }

    [Required]
    public List<Feature> Features { get; set; } = new();

    public OutputConfig? OutputConfig { get; set; }
}

public class GetOperationRequest
{
    [Required]
    public string ProjectId { get; set; } = string.Empty;

    [Required]
    public string OperationName { get; set; } = string.Empty;
}
