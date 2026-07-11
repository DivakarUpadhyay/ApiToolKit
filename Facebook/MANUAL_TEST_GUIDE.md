# Facebook API Manual Test Guide

**Base URL:** `http://localhost:5150`  
**Token:** `EAAO2NnWebZBcBRyZBG3ZCYiitYDjQZA6hDjW3CQYuwnKPiXnn1RjjKq5y3eHOZAXr0PJaiEiDOZBr1U3hzKZBxQhIzrtB5NozXXL8091iQjvoW4czYbJWQMKeDpZCnKI20HZALur6XrmMVbydwP54IbBjfUFaah7E9psvKWtIPp85WITVPESkmgOyvGoRwW2sIMUJ2U7g`  
**User ID:** `122108821923364890`  
**Business ID:** `2240917336659449`  
**App ID:** `1044769947938791`

> All GET requests: no body needed. POST/PUT: set `Content-Type: application/json`.

---

## 1. AUTH ✅ (5/5)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/auth/auth-url` | `{"scope":["public_profile","business_management"],"state":"login1"}` |
| 2 | GET | `/api/facebook/auth/app-token` | — |
| 3 | POST | `/api/facebook/auth/token/debug` | `{"inputToken":"EAAO2NnWebZBc..."}` |
| 4 | POST | `/api/facebook/auth/token/refresh` | `{"accessToken":"EAAO2NnWebZBc..."}` |
| 5 | POST | `/api/facebook/auth/token/long-lived` | `{"shortLivedToken":"EAAO2NnWebZBc..."}` |

---

## 2. USER ✅ (7/7)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | GET | `/api/facebook/user/122108821923364890/profile?accessToken=TOKEN` | — |
| 2 | GET | `/api/facebook/user/122108821923364890/friends?accessToken=TOKEN` | — |
| 3 | GET | `/api/facebook/user/122108821923364890/permissions?accessToken=TOKEN` | — |
| 4 | GET | `/api/facebook/user/122108821923364890/picture?accessToken=TOKEN` | — |
| 5 | GET | `/api/facebook/user/122108821923364890/feed?accessToken=TOKEN` | — |
| 6 | GET | `/api/facebook/user/122108821923364890/pages?accessToken=TOKEN` | — |
| 7 | GET | `/api/facebook/user/122108821923364890/ad-accounts?accessToken=TOKEN` | — |

---

## 3. BUSINESS ✅ (11/11)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | GET | `/api/facebook/business/2240917336659449?accessToken=TOKEN` | — |
| 2 | GET | `/api/facebook/business/2240917336659449/pages?accessToken=TOKEN` | — |
| 3 | GET | `/api/facebook/business/2240917336659449/ad-accounts?accessToken=TOKEN` | — |
| 4 | GET | `/api/facebook/business/2240917336659449/instagram-accounts?accessToken=TOKEN` | — |
| 5 | GET | `/api/facebook/business/2240917336659449/catalogs?accessToken=TOKEN` | — |
| 6 | GET | `/api/facebook/business/2240917336659449/users?accessToken=TOKEN` | — |
| 7 | GET | `/api/facebook/business/2240917336659449/system-users?accessToken=TOKEN` | — |
| 8 | GET | `/api/facebook/business/2240917336659449/client-pages?accessToken=TOKEN` | — |
| 9 | POST | `/api/facebook/business/2240917336659449/users?accessToken=TOKEN` | `{"userId":"122108821923364890","role":"EMPLOYEE","email":"user@example.com"}` |
| 10 | DELETE | `/api/facebook/business/2240917336659449/users/122108821923364890?accessToken=TOKEN` | — |
| 11 | POST | `/api/facebook/business/2240917336659449/claim-page?accessToken=TOKEN` | `{"pageId":"123456789"}` |

---

## 4. PAGE ⚠️ (6/12) — write endpoints need pages_manage_posts scope

| # | Method | URL | Body | Status |
|---|--------|-----|------|--------|
| 1 | GET | `/api/facebook/page/categories?accessToken=TOKEN` | — | ✅ |
| 2 | GET | `/api/facebook/page/user/122108821923364890/pages?accessToken=TOKEN` | — | ✅ |
| 3 | GET | `/api/facebook/page/123456789/info?accessToken=TOKEN` | — | ✅ |
| 4 | GET | `/api/facebook/page/123456789/access-token?accessToken=TOKEN` | — | ❌ needs managed page |
| 5 | PUT | `/api/facebook/page/123456789/settings?accessToken=TOKEN` | `{"pageId":"123456789","setting":"MESSAGING","value":"true"}` | ✅ |
| 6 | POST | `/api/facebook/page/123456789/post?accessToken=TOKEN` | `{"message":"Test post"}` | ❌ needs pages_manage_posts |
| 7 | POST | `/api/facebook/page/123456789/feed?accessToken=TOKEN` | `{"message":"Test feed"}` | ❌ needs pages_manage_posts |
| 8 | POST | `/api/facebook/page/123456789/photos?accessToken=TOKEN` | `{"url":"https://example.com/img.jpg","caption":"test"}` | ❌ needs pages_manage_media |
| 9 | POST | `/api/facebook/page/123456789/videos?accessToken=TOKEN` | `{"fileUrl":"https://example.com/video.mp4","title":"test"}` | ❌ needs pages_manage_media |
| 10 | GET | `/api/facebook/page/123456789/events?accessToken=TOKEN` | — | ✅ |
| 11 | PUT | `/api/facebook/page/123456789/picture?accessToken=TOKEN` | `{"url":"https://example.com/img.jpg"}` | ❌ needs pages_manage_posts |
| 12 | GET | `/api/facebook/page/123456789/roles?accessToken=TOKEN` | — | ✅ |

---

## 5. POST ⚠️ (6/7)

| # | Method | URL | Body | Status |
|---|--------|-----|------|--------|
| 1 | GET | `/api/facebook/post/122108821923364890?accessToken=TOKEN` | — | ✅ |
| 2 | PUT | `/api/facebook/post/122108821923364890?accessToken=TOKEN` | `{"message":"updated message"}` | ❌ needs pages_manage_posts |
| 3 | DELETE | `/api/facebook/post/122108821923364890?accessToken=TOKEN` | — | ✅ |
| 4 | POST | `/api/facebook/post/feed?accessToken=TOKEN` | `{"pageId":"123456789","message":"Hello","link":""}` | ✅ |
| 5 | POST | `/api/facebook/post/122108821923364890/comments?accessToken=TOKEN` | `{"objectId":"122108821923364890","limit":10}` | ✅ |
| 6 | POST | `/api/facebook/post/122108821923364890/reactions?accessToken=TOKEN` | `{"objectId":"122108821923364890","type":"LIKE"}` | ✅ |
| 7 | POST | `/api/facebook/post/122108821923364890/publish?accessToken=TOKEN` | `{"postId":"122108821923364890"}` | ✅ |

---

## 6. COMMENT ⚠️ (5/7)

| # | Method | URL | Body | Status |
|---|--------|-----|------|--------|
| 1 | GET | `/api/facebook/comment/123456789?accessToken=TOKEN` | — | ✅ |
| 2 | POST | `/api/facebook/comment/list?accessToken=TOKEN` | `{"objectId":"122108821923364890","limit":10}` | ✅ |
| 3 | PUT | `/api/facebook/comment/123456789?accessToken=TOKEN` | `{"message":"updated comment"}` | ❌ needs pages_manage_posts |
| 4 | DELETE | `/api/facebook/comment/123456789?accessToken=TOKEN` | — | ✅ |
| 5 | POST | `/api/facebook/comment/123456789/hide?accessToken=TOKEN` | `{"isHidden":true}` | ❌ needs pages_manage_posts |
| 6 | GET | `/api/facebook/comment/123456789/replies?accessToken=TOKEN` | — | ✅ |
| 7 | POST | `/api/facebook/comment/123456789/like?accessToken=TOKEN` | — | ✅ |

---

## 7. PHOTO ✅ (7/7)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/photo/upload?accessToken=TOKEN` | `{"pageId":"123456789","url":"https://example.com/img.jpg","caption":"test"}` |
| 2 | GET | `/api/facebook/photo/123456789?accessToken=TOKEN` | — |
| 3 | POST | `/api/facebook/photo/list?accessToken=TOKEN` | `{"pageId":"123456789","limit":10}` |
| 4 | DELETE | `/api/facebook/photo/123456789?accessToken=TOKEN` | — |
| 5 | GET | `/api/facebook/photo/123456789/albums?accessToken=TOKEN` | — |
| 6 | POST | `/api/facebook/photo/album?accessToken=TOKEN` | `{"pageId":"123456789","name":"Test Album","message":"desc"}` |
| 7 | GET | `/api/facebook/photo/123456789/photos?accessToken=TOKEN` | — |

---

## 8. VIDEO ✅ (7/7)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/video/upload?accessToken=TOKEN` | `{"pageId":"123456789","fileUrl":"https://example.com/vid.mp4","title":"Test","description":"desc"}` |
| 2 | GET | `/api/facebook/video/123456789?accessToken=TOKEN` | — |
| 3 | POST | `/api/facebook/video/list?accessToken=TOKEN` | `{"pageId":"123456789","limit":10}` |
| 4 | PUT | `/api/facebook/video/123456789?accessToken=TOKEN` | `{"title":"Updated","description":"updated desc"}` |
| 5 | DELETE | `/api/facebook/video/123456789?accessToken=TOKEN` | — |
| 6 | GET | `/api/facebook/video/123456789/status?accessToken=TOKEN` | — |
| 7 | GET | `/api/facebook/video/123456789/thumbnails?accessToken=TOKEN` | — |

---

## 9. EVENT ⚠️ (3/6)

| # | Method | URL | Body | Status |
|---|--------|-----|------|--------|
| 1 | GET | `/api/facebook/event/123456789?accessToken=TOKEN` | — | ✅ |
| 2 | PUT | `/api/facebook/event/123456789?accessToken=TOKEN` | `{"name":"Updated Event","startTime":"2026-08-01T10:00:00"}` | ❌ needs create_event |
| 3 | DELETE | `/api/facebook/event/123456789?accessToken=TOKEN` | — | ✅ |
| 4 | GET | `/api/facebook/event/123456789/events?accessToken=TOKEN` | — | ✅ |
| 5 | POST | `/api/facebook/event/123456789/attendees?accessToken=TOKEN` | `{"status":"attending"}` | ❌ needs create_event |
| 6 | POST | `/api/facebook/event/123456789/rsvp?accessToken=TOKEN` | `{"rsvp":"attending"}` | ❌ needs rsvp_event |

---

## 10. GROUP ⚠️ (2/6) — write endpoints need Facebook Groups API approval

| # | Method | URL | Body | Status |
|---|--------|-----|------|--------|
| 1 | GET | `/api/facebook/group/123456789?accessToken=TOKEN` | — | ✅ |
| 2 | GET | `/api/facebook/group/user/122108821923364890/groups?accessToken=TOKEN` | — | ✅ |
| 3 | POST | `/api/facebook/group/123456789/feed?accessToken=TOKEN` | `{"message":"Test post"}` | ❌ Groups API |
| 4 | POST | `/api/facebook/group/123456789/members?accessToken=TOKEN` | `{"memberId":"122108821923364890"}` | ❌ Groups API |
| 5 | POST | `/api/facebook/group/123456789/post?accessToken=TOKEN` | `{"message":"Hello group","link":""}` | ❌ Groups API |
| 6 | POST | `/api/facebook/group/123456789/albums?accessToken=TOKEN` | `{"name":"Group Album"}` | ❌ Groups API |

---

## 11. REACTION ✅ (2/2)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/reaction/list?accessToken=TOKEN` | `{"objectId":"122108821923364890","limit":10}` |
| 2 | DELETE | `/api/facebook/reaction/122108821923364890?accessToken=TOKEN` | — |

---

## 12. INSIGHT ✅ (6/6)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/insight/page?accessToken=TOKEN` | `{"pageId":"2240917336659449","metric":["page_fans"],"period":"day"}` |
| 2 | POST | `/api/facebook/insight/video?accessToken=TOKEN` | `{"videoId":"123456789","metric":"total_video_views","period":"day"}` |
| 3 | POST | `/api/facebook/insight/ad?accessToken=TOKEN` | `{"adId":"123456789","accountId":"act_123456789","fields":["impressions","clicks"]}` |
| 4 | GET | `/api/facebook/insight/campaign/123456789?accessToken=TOKEN` | — |
| 5 | GET | `/api/facebook/insight/adset/123456789?accessToken=TOKEN` | — |
| 6 | GET | `/api/facebook/insight/account/act_123456789?accessToken=TOKEN` | — |

---

## 13. AD ACCOUNT ✅ (6/6)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | GET | `/api/facebook/ad-account/act_123456789?accessToken=TOKEN` | — |
| 2 | POST | `/api/facebook/ad-account/user/ad-accounts?accessToken=TOKEN` | `{"userId":"122108821923364890"}` |
| 3 | GET | `/api/facebook/ad-account/act_123456789/billing?accessToken=TOKEN` | — |
| 4 | POST | `/api/facebook/ad-account/act_123456789/users?accessToken=TOKEN` | `{"accountId":"act_123456789","userId":"122108821923364890","role":"ADMIN"}` |
| 5 | POST | `/api/facebook/ad-account/act_123456789/campaigns?accessToken=TOKEN` | `{"accountId":"act_123456789","limit":10}` |
| 6 | GET | `/api/facebook/ad-account/act_123456789/creatives?accessToken=TOKEN` | — |

---

## 14. CAMPAIGN ✅ (7/7)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/campaign?accessToken=TOKEN` | `{"accountId":"act_123456789","name":"Test Campaign","objective":"OUTCOME_AWARENESS","status":"PAUSED","dailyBudget":1000}` |
| 2 | GET | `/api/facebook/campaign/123456789?accessToken=TOKEN` | — |
| 3 | POST | `/api/facebook/campaign/list?accessToken=TOKEN` | `{"accountId":"act_123456789","limit":10}` |
| 4 | PUT | `/api/facebook/campaign/123456789?accessToken=TOKEN` | `{"campaignId":"123456789","name":"Updated Campaign","status":"ACTIVE"}` |
| 5 | DELETE | `/api/facebook/campaign/123456789?accessToken=TOKEN` | — |
| 6 | GET | `/api/facebook/campaign/123456789/adsets?accessToken=TOKEN` | — |
| 7 | GET | `/api/facebook/campaign/123456789/ads?accessToken=TOKEN` | — |

---

## 15. AD SET ✅ (6/6)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/adset?accessToken=TOKEN` | `{"campaignId":"123456789","accountId":"act_123456789","name":"Test AdSet","dailyBudget":500,"bidAmount":100,"billingEvent":"IMPRESSIONS","optimizationGoal":"REACH","targeting":{}}` |
| 2 | GET | `/api/facebook/adset/123456789?accessToken=TOKEN` | — |
| 3 | POST | `/api/facebook/adset/list?accessToken=TOKEN` | `{"campaignId":"123456789","limit":10}` |
| 4 | PUT | `/api/facebook/adset/123456789?accessToken=TOKEN` | `{"adSetId":"123456789","name":"Updated AdSet","status":"ACTIVE"}` |
| 5 | DELETE | `/api/facebook/adset/123456789?accessToken=TOKEN` | — |
| 6 | GET | `/api/facebook/adset/123456789/ads?accessToken=TOKEN` | — |

---

## 16. AD ✅ (8/8)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/ad?accessToken=TOKEN` | `{"adSetId":"123456789","accountId":"act_123456789","name":"Test Ad","status":"PAUSED","creative":{}}` |
| 2 | GET | `/api/facebook/ad/123456789?accessToken=TOKEN` | — |
| 3 | POST | `/api/facebook/ad/list?accessToken=TOKEN` | `{"adSetId":"123456789","limit":10}` |
| 4 | PUT | `/api/facebook/ad/123456789?accessToken=TOKEN` | `{"adId":"123456789","name":"Updated Ad","status":"ACTIVE"}` |
| 5 | DELETE | `/api/facebook/ad/123456789?accessToken=TOKEN` | — |
| 6 | POST | `/api/facebook/ad/creative?accessToken=TOKEN` | `{"accountId":"act_123456789","name":"Test Creative","objectStorySpec":{}}` |
| 7 | GET | `/api/facebook/ad/123456789/preview?adFormat=DESKTOP_FEED_STANDARD&accessToken=TOKEN` | — |
| 8 | GET | `/api/facebook/ad/account/act_123456789/creatives?accessToken=TOKEN` | — |

---

## 17. PIXEL ✅ (8/8)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/pixel/account/act_123456789?accessToken=TOKEN` | `{"name":"My Test Pixel"}` |
| 2 | GET | `/api/facebook/pixel/account/act_123456789?accessToken=TOKEN` | — |
| 3 | GET | `/api/facebook/pixel/123456789?accessToken=TOKEN` | — |
| 4 | PUT | `/api/facebook/pixel/123456789?accessToken=TOKEN` | `{"name":"Updated Pixel"}` |
| 5 | GET | `/api/facebook/pixel/123456789/stats?accessToken=TOKEN` | — |
| 6 | GET | `/api/facebook/pixel/123456789/audiences?accessToken=TOKEN` | — |
| 7 | POST | `/api/facebook/pixel/123456789/share?accessToken=TOKEN` | `{"targetAdAccountId":"act_123456789"}` |
| 8 | GET | `/api/facebook/pixel/123456789/diagnostics?accessToken=TOKEN` | — |

---

## 18. CAPI ✅ (5/5)

> Note: token goes inside the JSON body, not as query param.

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/capi/events` | `{"PixelId":"123456789","AccessToken":"TOKEN","Data":[{"EventName":"Purchase","EventTime":1700000000}]}` |
| 2 | POST | `/api/facebook/capi/events/purchase` | `{"PixelId":"123456789","AccessToken":"TOKEN","EventTime":1700000000,"Value":29.99,"Currency":"USD"}` |
| 3 | POST | `/api/facebook/capi/events/add-to-cart` | `{"PixelId":"123456789","AccessToken":"TOKEN","EventTime":1700000000,"Value":50.0,"Currency":"USD"}` |
| 4 | POST | `/api/facebook/capi/events/page-view` | `{"PixelId":"123456789","AccessToken":"TOKEN","EventTime":1700000000,"EventSourceUrl":"https://test.com"}` |
| 5 | POST | `/api/facebook/capi/events/lead` | `{"PixelId":"123456789","AccessToken":"TOKEN","EventTime":1700000000,"Email":"test@test.com"}` |

---

## 19. CATALOG ✅ (11/11)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/catalog?accessToken=TOKEN` | `{"businessId":"2240917336659449","name":"Test Catalog","verticalType":"commerce"}` |
| 2 | GET | `/api/facebook/catalog/123456789?accessToken=TOKEN` | — |
| 3 | POST | `/api/facebook/catalog/list?accessToken=TOKEN` | `{"businessId":"2240917336659449"}` |
| 4 | DELETE | `/api/facebook/catalog/123456789?accessToken=TOKEN` | — |
| 5 | POST | `/api/facebook/catalog/123456789/product?accessToken=TOKEN` | `{"catalogId":"123456789","retailerId":"SKU001","name":"Test Product","description":"desc","price":100,"currency":"USD","url":"https://example.com","imageUrl":"https://example.com/img.jpg","availability":"in stock","condition":"new","brand":"TestBrand"}` |
| 6 | GET | `/api/facebook/catalog/product/prod123?accessToken=TOKEN` | — |
| 7 | POST | `/api/facebook/catalog/123456789/products?accessToken=TOKEN` | `{"catalogId":"123456789","limit":25}` |
| 8 | PUT | `/api/facebook/catalog/product/prod123?accessToken=TOKEN` | `{"productId":"prod123","price":200,"availability":"out of stock"}` |
| 9 | DELETE | `/api/facebook/catalog/product/prod123?accessToken=TOKEN` | — |
| 10 | POST | `/api/facebook/catalog/123456789/product-set?accessToken=TOKEN` | `{"catalogId":"123456789","name":"Test Set","filter":{}}` |
| 11 | GET | `/api/facebook/catalog/123456789/product-sets?accessToken=TOKEN` | — |

---

## 20. INSTAGRAM ⚠️ (35/38)

| # | Method | URL | Body | Status |
|---|--------|-----|------|--------|
| 1 | GET | `/api/facebook/instagram/123456789/account?accessToken=TOKEN` | — | ✅ |
| 2 | GET | `/api/facebook/instagram/page/123456789/instagram?accessToken=TOKEN` | — | ❌ needs page token |
| 3 | POST | `/api/facebook/instagram/media?accessToken=TOKEN` | `{"igUserId":"123456789","imageUrl":"https://example.com/img.jpg","caption":"Test"}` | ✅ |
| 4 | POST | `/api/facebook/instagram/media/publish?accessToken=TOKEN` | `{"igUserId":"123456789","creationId":"123456789"}` | ✅ |
| 5 | POST | `/api/facebook/instagram/media/list?accessToken=TOKEN` | `{"igUserId":"123456789","limit":10}` | ✅ |
| 6 | POST | `/api/facebook/instagram/media/insights?accessToken=TOKEN` | `{"mediaId":"123456789","metric":["impressions","reach"]}` | ✅ |
| 7 | POST | `/api/facebook/instagram/hashtag/search?accessToken=TOKEN` | `{"igUserId":"123456789","hashtagName":"sunset"}` | ✅ |
| 8 | POST | `/api/facebook/instagram/hashtag/media?accessToken=TOKEN` | `{"igUserId":"123456789","hashtagId":"123456789","type":"top_media"}` | ✅ |
| 9 | GET | `/api/facebook/instagram/media/123456789/comments?accessToken=TOKEN` | — | ✅ |
| 10 | POST | `/api/facebook/instagram/comment/123456789/reply?message=Hello&accessToken=TOKEN` | — | ✅ |
| 11 | DELETE | `/api/facebook/instagram/comment/123456789?accessToken=TOKEN` | — | ✅ |
| 12 | POST | `/api/facebook/instagram/business-discovery?accessToken=TOKEN` | `{"igUserId":"123456789","username":"testuser","accessToken":"TOKEN"}` | ❌ needs page token |
| 13 | GET | `/api/facebook/instagram/123456789/insights?metrics=follower_count&period=day&accessToken=TOKEN` | — | ✅ |
| 14 | POST | `/api/facebook/instagram/story` | `{"igUserId":"123456789","imageUrl":"https://example.com/img.jpg","accessToken":"TOKEN"}` | ✅ |
| 15 | POST | `/api/facebook/instagram/reel` | `{"igUserId":"123456789","videoUrl":"https://example.com/vid.mp4","caption":"Test reel","accessToken":"TOKEN"}` | ✅ |
| 16 | POST | `/api/facebook/instagram/carousel` | `{"igUserId":"123456789","items":[{"imageUrl":"https://example.com/img.jpg","isCarouselItem":true}],"caption":"Test","accessToken":"TOKEN"}` | ✅ |
| 17 | POST | `/api/facebook/instagram/container/status` | `{"containerId":"123456789","accessToken":"TOKEN"}` | ✅ |
| 18 | POST | `/api/facebook/instagram/publishing-limit` | `{"igUserId":"123456789","accessToken":"TOKEN"}` | ✅ |
| 19 | POST | `/api/facebook/instagram/mentions` | `{"igUserId":"123456789","limit":10,"accessToken":"TOKEN"}` | ✅ |
| 20 | POST | `/api/facebook/instagram/tagged-media` | `{"igUserId":"123456789","limit":10,"accessToken":"TOKEN"}` | ✅ |
| 21 | POST | `/api/facebook/instagram/dm/send` | `{"igUserId":"123456789","recipientIgId":"987654321","message":"Hello","accessToken":"TOKEN"}` | ✅ |
| 22 | POST | `/api/facebook/instagram/dm/conversations` | `{"igUserId":"123456789","limit":10,"accessToken":"TOKEN"}` | ✅ |
| 23 | GET | `/api/facebook/instagram/123456789/stories?accessToken=TOKEN` | — | ✅ |
| 24 | GET | `/api/facebook/instagram/123456789/live-media?accessToken=TOKEN` | — | ✅ |
| 25 | GET | `/api/facebook/instagram/media/123456789/children?accessToken=TOKEN` | — | ✅ |
| 26 | DELETE | `/api/facebook/instagram/media/123456789?accessToken=TOKEN` | — | ✅ |
| 27 | POST | `/api/facebook/instagram/media/123456789/toggle-comments?commentsEnabled=true&accessToken=TOKEN` | — | ✅ |
| 28 | GET | `/api/facebook/instagram/123456789/recently-searched-hashtags?accessToken=TOKEN` | — | ✅ |
| 29 | GET | `/api/facebook/instagram/123456789/mentioned-comment?commentId=123&accessToken=TOKEN` | — | ✅ |
| 30 | GET | `/api/facebook/instagram/123456789/mentioned-media?mediaId=123&accessToken=TOKEN` | — | ✅ |
| 31 | GET | `/api/facebook/instagram/media/123456789/detail?accessToken=TOKEN` | — | ✅ |
| 32 | POST | `/api/facebook/instagram/comment/123456789/hide?hide=true&accessToken=TOKEN` | — | ✅ |
| 33 | GET | `/api/facebook/instagram/comment/123456789/replies?accessToken=TOKEN` | — | ✅ |
| 34 | GET | `/api/facebook/instagram/comment/123456789?accessToken=TOKEN` | — | ✅ |
| 35 | POST | `/api/facebook/instagram/media/123456789/comment` | `{"mediaId":"123456789","message":"Great post!","accessToken":"TOKEN"}` | ✅ |
| 36 | POST | `/api/facebook/instagram/business-discovery/media` | `{"igUserId":"123456789","username":"testuser","limit":10,"accessToken":"TOKEN"}` | ❌ needs page token |
| 37 | POST | `/api/facebook/instagram/webhook/subscribe` | `{"appId":"1044769947938791","igUserId":"123456789","callbackUrl":"https://example.com/webhook","verifyToken":"secret","accessToken":"TOKEN"}` | ✅ |
| 38 | POST | `/api/facebook/instagram/conversation/messages` | `{"igUserId":"123456789","conversationId":"123456789","accessToken":"TOKEN"}` | ✅ |

---

## 21. INSTAGRAM SHOPPING ✅ (10/10)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | GET | `/api/facebook/instagram-shopping/123456789/catalog?accessToken=TOKEN` | — |
| 2 | POST | `/api/facebook/instagram-shopping/media/123456789/product-tags?accessToken=TOKEN` | `{"tags":[{"productId":"123","x":0.5,"y":0.5}]}` |
| 3 | GET | `/api/facebook/instagram-shopping/media/123456789/product-tags?accessToken=TOKEN` | — |
| 4 | DELETE | `/api/facebook/instagram-shopping/media/123456789/product-tags?accessToken=TOKEN` | — |
| 5 | GET | `/api/facebook/instagram-shopping/123456789/tagged-media?accessToken=TOKEN` | — |
| 6 | POST | `/api/facebook/instagram-shopping/123456789/appeal?accessToken=TOKEN` | `{"productId":"123","appealReason":"Product correctly represents my account"}` |
| 7 | GET | `/api/facebook/instagram-shopping/123456789/eligibility?accessToken=TOKEN` | — |
| 8 | GET | `/api/facebook/instagram-shopping/catalog/123456789/products?accessToken=TOKEN` | — |
| 9 | GET | `/api/facebook/instagram-shopping/123456789/shops?accessToken=TOKEN` | — |
| 10 | GET | `/api/facebook/instagram-shopping/123456789/available-catalogs?accessToken=TOKEN` | — |

---

## 22. LIVE ✅ (8/8)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/live/123456789?accessToken=TOKEN` | `{"title":"Test Live","description":"desc","privacy":"EVERYONE"}` |
| 2 | GET | `/api/facebook/live/123456789/videos?accessToken=TOKEN` | — |
| 3 | GET | `/api/facebook/live/video/123456789?accessToken=TOKEN` | — |
| 4 | PUT | `/api/facebook/live/video/123456789?accessToken=TOKEN` | `{"title":"Updated Live"}` |
| 5 | POST | `/api/facebook/live/video/123456789/end?accessToken=TOKEN` | — |
| 6 | DELETE | `/api/facebook/live/video/123456789?accessToken=TOKEN` | — |
| 7 | GET | `/api/facebook/live/video/123456789/errors?accessToken=TOKEN` | — |
| 8 | GET | `/api/facebook/live/video/123456789/blocked-users?accessToken=TOKEN` | — |

---

## 23. MESSENGER ✅ (9/9)

> Note: send/mark-seen/typing/user-profile use `pageAccessToken` query param, others use `accessToken`.

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/messenger/send?pageAccessToken=TOKEN` | `{"recipientId":"PSID123","messageText":"Hello"}` |
| 2 | POST | `/api/facebook/messenger/conversations?accessToken=TOKEN` | `{"pageId":"123456789","limit":25}` |
| 3 | POST | `/api/facebook/messenger/messages?accessToken=TOKEN` | `{"conversationId":"123456789","limit":25}` |
| 4 | POST | `/api/facebook/messenger/user/profile?pageAccessToken=TOKEN` | `{"psid":"PSID123","pageId":"123456789"}` |
| 5 | POST | `/api/facebook/messenger/mark-seen?pageAccessToken=TOKEN` | `{"recipientId":"PSID123"}` |
| 6 | POST | `/api/facebook/messenger/typing?pageAccessToken=TOKEN` | `{"recipientId":"PSID123","action":"typing_on"}` |
| 7 | POST | `/api/facebook/messenger/subscribe?accessToken=TOKEN` | `{"pageId":"123456789","pageAccessToken":"TOKEN"}` |
| 8 | GET | `/api/facebook/messenger/page/123456789/subscriptions?accessToken=TOKEN` | — |
| 9 | POST | `/api/facebook/messenger/page/123456789/profile?accessToken=TOKEN` | `{"greeting":"Hello","getStartedPayload":"START"}` |

---

## 24. THREADS ⚠️ (10/15) — POST creation needs Threads OAuth token

| # | Method | URL | Body | Status |
|---|--------|-----|------|--------|
| 1 | GET | `/api/facebook/threads/profile?accessToken=TOKEN` | — | ✅ |
| 2 | GET | `/api/facebook/threads/posts?accessToken=TOKEN` | — | ✅ |
| 3 | POST | `/api/facebook/threads/posts/text?accessToken=TOKEN` | `{"text":"Hello Threads"}` | ❌ needs Threads token |
| 4 | POST | `/api/facebook/threads/posts/image?accessToken=TOKEN` | `{"imageUrl":"https://example.com/img.jpg","text":"caption"}` | ❌ needs Threads token |
| 5 | POST | `/api/facebook/threads/posts/video?accessToken=TOKEN` | `{"videoUrl":"https://example.com/vid.mp4","text":"caption"}` | ❌ needs Threads token |
| 6 | POST | `/api/facebook/threads/posts/carousel?accessToken=TOKEN` | `{"children":[],"text":"caption"}` | ❌ needs Threads token |
| 7 | POST | `/api/facebook/threads/posts/publish?accessToken=TOKEN` | `{"creationId":"123456789"}` | ✅ |
| 8 | GET | `/api/facebook/threads/posts/123456789?accessToken=TOKEN` | — | ✅ |
| 9 | DELETE | `/api/facebook/threads/posts/123456789?accessToken=TOKEN` | — | ✅ |
| 10 | POST | `/api/facebook/threads/posts/123456789/reply?accessToken=TOKEN` | `{"text":"my reply"}` | ❌ needs Threads token |
| 11 | GET | `/api/facebook/threads/posts/123456789/replies?accessToken=TOKEN` | — | ✅ |
| 12 | GET | `/api/facebook/threads/posts/123456789/conversation?accessToken=TOKEN` | — | ✅ |
| 13 | GET | `/api/facebook/threads/insights?accessToken=TOKEN` | — | ✅ |
| 14 | GET | `/api/facebook/threads/posts/123456789/insights?accessToken=TOKEN` | — | ✅ |
| 15 | GET | `/api/facebook/threads/publishing-limit?accessToken=TOKEN` | — | ✅ |

---

## 25. WEBHOOK ✅ (4/4)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | POST | `/api/facebook/webhook/subscribe?accessToken=TOKEN` | `{"objectType":"page","callbackUrl":"https://example.com/webhook","verifyToken":"secret","fields":["feed"],"appId":"1044769947938791"}` |
| 2 | POST | `/api/facebook/webhook/subscriptions?accessToken=TOKEN` | `{"appId":"1044769947938791"}` |
| 3 | GET | `/api/facebook/webhook/verify?hub.mode=subscribe&hub.verify_token=secret&hub.challenge=CHALLENGE123` | — |
| 4 | POST | `/api/facebook/webhook/payload` | `{"object":"page","entry":[]}` |

---

## 26. COMMERCE ✅ (18/18)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | GET | `/api/facebook/commerce/shops/123456789?accessToken=TOKEN` | — |
| 2 | GET | `/api/facebook/commerce/shops/detail/123456789?accessToken=TOKEN` | — |
| 3 | GET | `/api/facebook/commerce/orders/123456789?accessToken=TOKEN` | — |
| 4 | GET | `/api/facebook/commerce/orders/detail/order123?accessToken=TOKEN` | — |
| 5 | POST | `/api/facebook/commerce/orders/order123/acknowledge?accessToken=TOKEN` | `{"merchantOrderReference":"REF-ORD-123456"}` |
| 6 | POST | `/api/facebook/commerce/orders/order123/shipments?accessToken=TOKEN` | `{"trackingNumber":"TRK123","carrierName":"UPS","shippingMethod":"STANDARD"}` |
| 7 | POST | `/api/facebook/commerce/orders/order123/cancel?accessToken=TOKEN` | `{"reasonCode":"CUSTOMER_REQUESTED"}` |
| 8 | POST | `/api/facebook/commerce/orders/order123/refund?accessToken=TOKEN` | `{"refundAmount":10.00,"currency":"USD","reasonText":"Customer return"}` |
| 9 | GET | `/api/facebook/commerce/orders/order123/fulfillments?accessToken=TOKEN` | — |
| 10 | GET | `/api/facebook/commerce/orders/order123/returns?accessToken=TOKEN` | — |
| 11 | GET | `/api/facebook/commerce/account/123456789?accessToken=TOKEN` | — |
| 12 | GET | `/api/facebook/commerce/account/123456789/settings?accessToken=TOKEN` | — |
| 13 | GET | `/api/facebook/commerce/account/123456789/tax-settings?accessToken=TOKEN` | — |
| 14 | PUT | `/api/facebook/commerce/account/123456789/tax-settings?accessToken=TOKEN` | `{"taxRemitState":"ENABLED"}` |
| 15 | GET | `/api/facebook/commerce/account/123456789/return-settings?accessToken=TOKEN` | — |
| 16 | PUT | `/api/facebook/commerce/account/123456789/return-settings?accessToken=TOKEN` | `{"returnDays":30}` |
| 17 | GET | `/api/facebook/commerce/account/123456789/shipping-profiles?accessToken=TOKEN` | — |
| 18 | GET | `/api/facebook/commerce/shops/123456789/collections?accessToken=TOKEN` | — |

---

## 27. PRODUCT FEED ✅ (8/8)

| # | Method | URL | Body / Note |
|---|--------|-----|-------------|
| 1 | POST | `/api/facebook/product-feed/123456789/feeds?accessToken=TOKEN` | `{"name":"Test Feed","url":"https://example.com/feed.csv","schedule":"DAILY"}` |
| 2 | GET | `/api/facebook/product-feed/123456789/feeds?accessToken=TOKEN` | — |
| 3 | GET | `/api/facebook/product-feed/feeds/feed123?accessToken=TOKEN` | — |
| 4 | DELETE | `/api/facebook/product-feed/feeds/feed123?accessToken=TOKEN` | — |
| 5 | POST | `/api/facebook/product-feed/feeds/feed123/upload?accessToken=TOKEN` | multipart/form-data: `file=@yourfile.csv` |
| 6 | GET | `/api/facebook/product-feed/feeds/feed123/uploads?accessToken=TOKEN` | — |
| 7 | GET | `/api/facebook/product-feed/uploads/upload123?accessToken=TOKEN` | — |
| 8 | GET | `/api/facebook/product-feed/feeds/feed123/errors?accessToken=TOKEN` | — |

---

## 28. WHATSAPP ⚠️ (44/46)

> Note: Most WhatsApp endpoints take token from DB config (no accessToken query param needed). Exceptions noted.

| # | Method | URL | Body | Status |
|---|--------|-----|------|--------|
| 1 | POST | `/api/facebook/whatsapp/messages/text` | `{"to":"919876543210","message":"Hello"}` | ✅ |
| 2 | POST | `/api/facebook/whatsapp/messages/template` | `{"to":"919876543210","templateName":"hello_world","languageCode":"en_US"}` | ✅ |
| 3 | POST | `/api/facebook/whatsapp/messages/image` | `{"to":"919876543210","imageUrl":"https://example.com/img.jpg"}` | ✅ |
| 4 | POST | `/api/facebook/whatsapp/messages/video` | `{"to":"919876543210","videoUrl":"https://example.com/vid.mp4"}` | ✅ |
| 5 | POST | `/api/facebook/whatsapp/messages/audio` | `{"to":"919876543210","audioUrl":"https://example.com/aud.mp3"}` | ✅ |
| 6 | POST | `/api/facebook/whatsapp/messages/document` | `{"to":"919876543210","documentUrl":"https://example.com/doc.pdf","filename":"doc.pdf"}` | ✅ |
| 7 | POST | `/api/facebook/whatsapp/messages/sticker` | `{"to":"919876543210","stickerUrl":"https://example.com/sticker.webp"}` | ✅ |
| 8 | POST | `/api/facebook/whatsapp/messages/location` | `{"to":"919876543210","latitude":28.6,"longitude":77.2,"name":"Delhi","address":"India"}` | ✅ |
| 9 | POST | `/api/facebook/whatsapp/messages/contact` | `{"to":"919876543210","contacts":[{"name":{"formattedName":"Test"},"phones":[{"phone":"123","type":"CELL"}]}]}` | ✅ |
| 10 | POST | `/api/facebook/whatsapp/messages/reaction` | `{"to":"919876543210","messageId":"msg123","emoji":"👍"}` | ✅ |
| 11 | POST | `/api/facebook/whatsapp/messages/interactive/buttons` | `{"to":"919876543210","bodyText":"Choose:","buttons":[{"id":"b1","title":"Yes"}]}` | ✅ |
| 12 | POST | `/api/facebook/whatsapp/messages/interactive/list` | `{"to":"919876543210","headerText":"Pick","bodyText":"Choose:","buttonText":"Select","sections":[{"title":"Opts","rows":[{"id":"1","title":"Option 1"}]}]}` | ✅ |
| 13 | POST | `/api/facebook/whatsapp/messages/interactive/product` | `{"to":"919876543210","catalogId":"cat1","productRetailerId":"prod1"}` | ✅ |
| 14 | POST | `/api/facebook/whatsapp/messages/interactive/catalog` | `{"to":"919876543210","catalogId":"cat1","bodyText":"Browse","sections":[{"title":"Items","productRetailerIds":["p1"]}]}` | ✅ |
| 15 | POST | `/api/facebook/whatsapp/messages/read` | `{"messageId":"msg123"}` | ✅ |
| 16 | POST | `/api/facebook/whatsapp/messages/interactive/location-request` | `{"to":"919876543210","bodyText":"Share location"}` | ✅ |
| 17 | POST | `/api/facebook/whatsapp/messages/interactive/cta-url` | `{"to":"919876543210","headerText":"Visit","bodyText":"Click","buttonDisplayText":"Open","buttonUrl":"https://example.com"}` | ✅ |
| 18 | POST | `/api/facebook/whatsapp/messages/interactive/flow` | `{"to":"919876543210","flowId":"flow1","flowToken":"token","bodyText":"Fill form"}` | ✅ |
| 19 | POST | `/api/facebook/whatsapp/media/upload` | `{"url":"https://example.com/img.jpg","type":"image/jpeg"}` | ❌ placeholder config |
| 20 | GET | `/api/facebook/whatsapp/media/media123?accessToken=TOKEN` | — | ✅ |
| 21 | DELETE | `/api/facebook/whatsapp/media/media123?accessToken=TOKEN` | — | ✅ |
| 22 | GET | `/api/facebook/whatsapp/phone-numbers?wabaId=waba123&accessToken=TOKEN` | — | ✅ |
| 23 | GET | `/api/facebook/whatsapp/phone-numbers/ph123?accessToken=TOKEN` | — | ✅ |
| 24 | POST | `/api/facebook/whatsapp/phone-numbers/ph123/request-code` | `{"codeMethod":"SMS"}` | ✅ |
| 25 | POST | `/api/facebook/whatsapp/phone-numbers/ph123/verify-code` | `{"code":"123456"}` | ✅ |
| 26 | POST | `/api/facebook/whatsapp/phone-numbers/ph123/register` | `{"pin":"123456"}` | ✅ |
| 27 | POST | `/api/facebook/whatsapp/phone-numbers/ph123/deregister` | `{}` | ✅ |
| 28 | POST | `/api/facebook/whatsapp/phone-numbers/ph123/2fa` | `{"pin":"123456"}` | ✅ |
| 29 | GET | `/api/facebook/whatsapp/business-profile?phoneNumberId=ph123&accessToken=TOKEN` | — | ✅ |
| 30 | POST | `/api/facebook/whatsapp/business-profile` | `{"phoneNumberId":"ph123","about":"Test business"}` | ✅ |
| 31 | POST | `/api/facebook/whatsapp/templates` | `{"wabaId":"waba123","name":"test_template","language":"en_US","category":"UTILITY","components":[]}` | ✅ |
| 32 | GET | `/api/facebook/whatsapp/templates?wabaId=waba123&accessToken=TOKEN` | — | ✅ |
| 33 | GET | `/api/facebook/whatsapp/templates/tmpl123?accessToken=TOKEN` | — | ✅ |
| 34 | PUT | `/api/facebook/whatsapp/templates/tmpl123` | `{"components":[]}` | ✅ |
| 35 | DELETE | `/api/facebook/whatsapp/templates/tmpl123?accessToken=TOKEN` | — | ✅ |
| 36 | POST | `/api/facebook/whatsapp/qr-codes` | `{"phoneNumberId":"ph123","prefilledMessage":"Hi","imageFormat":"PNG"}` | ✅ |
| 37 | GET | `/api/facebook/whatsapp/qr-codes?phoneNumberId=ph123&accessToken=TOKEN` | — | ✅ |
| 38 | GET | `/api/facebook/whatsapp/qr-codes/qr123?phoneNumberId=ph123&accessToken=TOKEN` | — | ✅ |
| 39 | PUT | `/api/facebook/whatsapp/qr-codes/qr123` | `{"prefilledMessage":"Updated"}` | ✅ |
| 40 | DELETE | `/api/facebook/whatsapp/qr-codes/qr123?phoneNumberId=ph123&accessToken=TOKEN` | — | ✅ |
| 41 | POST | `/api/facebook/whatsapp/webhook/subscribe` | `{"wabaId":"waba123","callbackUrl":"https://example.com/wh","verifyToken":"secret","fields":["messages"],"accessToken":"TOKEN"}` | ✅ |
| 42 | GET | `/api/facebook/whatsapp/webhook/verify?hub.mode=subscribe&hub.verify_token=secret&hub.challenge=CH123` | — | ❌ verify token mismatch |
| 43 | POST | `/api/facebook/whatsapp/webhook/payload` | `{"object":"whatsapp_business_account","entry":[]}` | ✅ |
| 44 | GET | `/api/facebook/whatsapp/waba/waba123?accessToken=TOKEN` | — | ✅ |
| 45 | POST | `/api/facebook/whatsapp/analytics` | `{"wabaId":"waba123","start":"1700000000","end":"1700086400","granularity":"DAY","accessToken":"TOKEN"}` | ✅ |
| 46 | POST | `/api/facebook/whatsapp/conversation-analytics` | `{"wabaId":"waba123","start":"1700000000","end":"1700086400","granularity":"DAY","accessToken":"TOKEN"}` | ✅ |

---

## 29. WHATSAPP CATALOG ✅ (7/7)

| # | Method | URL | Body |
|---|--------|-----|------|
| 1 | GET | `/api/facebook/whatsapp/catalog/waba123/catalogs?accessToken=TOKEN` | — |
| 2 | POST | `/api/facebook/whatsapp/catalog/waba123/catalogs?accessToken=TOKEN` | `{"name":"WA Catalog","vertical":"commerce"}` |
| 3 | GET | `/api/facebook/whatsapp/catalog/catalogs/cat123?accessToken=TOKEN` | — |
| 4 | GET | `/api/facebook/whatsapp/catalog/catalogs/cat123/products?accessToken=TOKEN` | — |
| 5 | POST | `/api/facebook/whatsapp/catalog/catalogs/cat123/products?accessToken=TOKEN` | `{"name":"Test Product","description":"A product","price":100,"currency":"USD","availability":"in stock","condition":"new"}` |
| 6 | GET | `/api/facebook/whatsapp/catalog/products/prod123?accessToken=TOKEN` | — |
| 7 | DELETE | `/api/facebook/whatsapp/catalog/products/prod123?accessToken=TOKEN` | — |
