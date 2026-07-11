namespace ApiToolKit.Application.DTOs.Shopee.GlobalProduct;

public class GetGlobalCategoryListRequest
{
    public string Language { get; set; } = string.Empty;
}

public class GetGlobalCategoryListResponse
{
    public List<GlobalCategoryInfo> CategoryList { get; set; } = new();
}

public class GlobalCategoryInfo
{
    public long CategoryId { get; set; }
    public long ParentCategoryId { get; set; }
    public string OriginalCategoryName { get; set; } = string.Empty;
    public string DisplayCategoryName { get; set; } = string.Empty;
    public bool HasChildren { get; set; }
}

public class GetCategoryAttributeRequest
{
    public long CategoryId { get; set; }
    public string Language { get; set; } = string.Empty;
}

public class GetCategoryAttributeResponse
{
    public List<AttributeInfo> Attributes { get; set; } = new();
}

public class AttributeInfo
{
    public long AttributeId { get; set; }
    public string OriginalAttributeName { get; set; } = string.Empty;
    public bool IsMandatory { get; set; }
    public string AttributeType { get; set; } = string.Empty;
    public List<AttributeValue> AttributeValueList { get; set; } = new();
}

public class AttributeValue
{
    public long ValueId { get; set; }
    public string OriginalValueName { get; set; } = string.Empty;
    public string DisplayValueName { get; set; } = string.Empty;
}

public class GetBrandListRequest
{
    public long CategoryId { get; set; }
    public int Offset { get; set; }
    public int PageSize { get; set; }
    public int Status { get; set; }
}

public class GetBrandListResponse
{
    public List<BrandInfo> BrandList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
    public int NextOffset { get; set; }
}

public class BrandInfo
{
    public long BrandId { get; set; }
    public string OriginalBrandName { get; set; } = string.Empty;
    public string DisplayBrandName { get; set; } = string.Empty;
}

public class GlobalItemImage
{
    public string Url { get; set; } = string.Empty;
}

public class GlobalItemAttribute
{
    public long AttributeId { get; set; }
    public List<AttributeValue> AttributeValueList { get; set; } = new();
}

public class GlobalItemLogistic
{
    public long LogisticsChannelId { get; set; }
    public bool Enabled { get; set; }
    public bool IsFree { get; set; }
}

public class AddGlobalItemRequest
{
    public string OriginalName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string OriginalSku { get; set; } = string.Empty;
    public long Price { get; set; }
    public int Stock { get; set; }
    public List<GlobalItemImage> Image { get; set; } = new();
    public long CategoryId { get; set; }
    public string Condition { get; set; } = string.Empty;
    public int ItemDangerous { get; set; }
    public List<GlobalItemLogistic> LogisticInfo { get; set; } = new();
    public List<GlobalItemAttribute> AttributeList { get; set; } = new();
}

public class AddGlobalItemResponse
{
    public long GlobalItemId { get; set; }
    public List<GlobalSkuInfo> GlobalSkuList { get; set; } = new();
}

public class GlobalSkuInfo
{
    public long GlobalModelId { get; set; }
    public string ModelSku { get; set; } = string.Empty;
}

public class CopyItemRequest
{
    public long ItemId { get; set; }
    public List<long> ShopIdList { get; set; } = new();
}

public class CopyItemResponse
{
    public List<long> SuccessShopIdList { get; set; } = new();
    public List<object> FailedList { get; set; } = new();
}

public class DeleteGlobalItemRequest
{
    public List<long> GlobalItemIdList { get; set; } = new();
}

public class DeleteGlobalItemResponse
{
    public List<long> FailedList { get; set; } = new();
}

public class GetGlobalItemListRequest
{
    public int Offset { get; set; }
    public int PageSize { get; set; }
    public string ItemStatus { get; set; } = string.Empty;
}

public class GetGlobalItemListResponse
{
    public List<GlobalItemBrief> GlobalItemList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
    public int NextOffset { get; set; }
}

public class GlobalItemBrief
{
    public long GlobalItemId { get; set; }
    public string OriginalName { get; set; } = string.Empty;
    public string GlobalSku { get; set; } = string.Empty;
    public long Price { get; set; }
    public int Stock { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class GetGlobalItemDetailRequest
{
    public List<long> GlobalItemIdList { get; set; } = new();
}

public class GetGlobalItemDetailResponse
{
    public List<GlobalItemDetail> GlobalItemList { get; set; } = new();
}

public class GlobalItemDetail
{
    public long GlobalItemId { get; set; }
    public string OriginalName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string OriginalSku { get; set; } = string.Empty;
    public long Price { get; set; }
    public int Stock { get; set; }
    public string Status { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public List<GlobalItemImage> Image { get; set; } = new();
}

public class UpdateGlobalItemRequest
{
    public long GlobalItemId { get; set; }
    public string OriginalName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long Price { get; set; }
    public int Stock { get; set; }
    public List<GlobalItemImage> Image { get; set; } = new();
}

public class UpdateGlobalItemResponse
{
    public long GlobalItemId { get; set; }
}

public class SearchGlobalItemRequest
{
    public string Keyword { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public int Offset { get; set; }
    public int PageSize { get; set; }
}

public class SearchGlobalItemResponse
{
    public List<GlobalItemBrief> GlobalItemList { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasNextPage { get; set; }
    public int NextOffset { get; set; }
}

public class InitTierVariationRequest
{
    public long GlobalItemId { get; set; }
    public List<TierVariationInfo> TierVariation { get; set; } = new();
}

public class InitTierVariationResponse
{
    public long GlobalItemId { get; set; }
    public List<GlobalModelInfo> GlobalModelList { get; set; } = new();
}

public class TierVariationInfo
{
    public string Name { get; set; } = string.Empty;
    public List<OptionInfo> OptionList { get; set; } = new();
}

public class OptionInfo
{
    public string Option { get; set; } = string.Empty;
    public GlobalItemImage Image { get; set; } = new();
}

public class GlobalModelInfo
{
    public long GlobalModelId { get; set; }
    public List<int> TierIndex { get; set; } = new();
    public long Price { get; set; }
    public int Stock { get; set; }
    public string ModelSku { get; set; } = string.Empty;
}

public class UpdateTierVariationRequest
{
    public long GlobalItemId { get; set; }
    public List<TierVariationInfo> TierVariation { get; set; } = new();
}

public class UpdateTierVariationResponse
{
    public long GlobalItemId { get; set; }
}

public class GetModelListRequest
{
    public long GlobalItemId { get; set; }
}

public class GetModelListResponse
{
    public List<TierVariationInfo> TierVariation { get; set; } = new();
    public List<GlobalModelInfo> GlobalModelList { get; set; } = new();
}

public class UpdateModelRequest
{
    public long GlobalItemId { get; set; }
    public List<GlobalModelPriceStock> GlobalModelList { get; set; } = new();
}

public class GlobalModelPriceStock
{
    public long GlobalModelId { get; set; }
    public long Price { get; set; }
    public int Stock { get; set; }
    public string ModelSku { get; set; } = string.Empty;
}

public class UpdateModelResponse
{
    public long GlobalItemId { get; set; }
    public List<GlobalModelInfo> GlobalModelList { get; set; } = new();
}

public class AddGlobalModelRequest
{
    public long GlobalItemId { get; set; }
    public List<GlobalModelPriceStock> GlobalModelList { get; set; } = new();
}

public class AddGlobalModelResponse
{
    public long GlobalItemId { get; set; }
    public List<GlobalModelInfo> GlobalModelList { get; set; } = new();
}

public class DeleteGlobalModelRequest
{
    public long GlobalItemId { get; set; }
    public List<long> GlobalModelIdList { get; set; } = new();
}

public class DeleteGlobalModelResponse
{
    public long GlobalItemId { get; set; }
}

public class GetGlobalItemInfoRequest
{
    public long GlobalItemId { get; set; }
}

public class GetGlobalItemInfoResponse
{
    public long GlobalItemId { get; set; }
    public string OriginalName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long Price { get; set; }
    public int Stock { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class UpdateGlobalItemStatusRequest
{
    public List<long> GlobalItemIdList { get; set; } = new();
    public string Status { get; set; } = string.Empty;
}

public class UpdateGlobalItemStatusResponse
{
    public List<long> FailedList { get; set; } = new();
}

public class GetShopItemInfoRequest
{
    public long GlobalItemId { get; set; }
    public long ShopId { get; set; }
}

public class GetShopItemInfoResponse
{
    public long ItemId { get; set; }
    public long ShopId { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class GetChannelListRequest
{
    public long GlobalItemId { get; set; }
}

public class GetChannelListResponse
{
    public List<GlobalItemLogistic> LogisticInfo { get; set; } = new();
}

public class UpdateGlobalItemLogisticRequest
{
    public long GlobalItemId { get; set; }
    public List<GlobalItemLogistic> LogisticInfo { get; set; } = new();
}

public class UpdateGlobalItemLogisticResponse
{
    public long GlobalItemId { get; set; }
}

public class BatchUpdatePriceRequest
{
    public List<long> GlobalItemIdList { get; set; } = new();
    public long Price { get; set; }
}

public class BatchUpdatePriceResponse
{
    public List<long> FailedList { get; set; } = new();
}

public class BatchUpdateStockRequest
{
    public List<long> GlobalItemIdList { get; set; } = new();
    public int Stock { get; set; }
}

public class BatchUpdateStockResponse
{
    public List<long> FailedList { get; set; } = new();
}
