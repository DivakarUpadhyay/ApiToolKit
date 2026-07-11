# Facebook API Test Results

**Tested:** 2026-07-10 (re-run after table clean)
**App ID:** 1044769947938791
**User ID:** 122108821923364890 (Nex Pandit)
**Business ID:** 2240917336659449 (NexMerchandise)
**Token Scopes:** ads_management, catalog_management, pages_show_list, pages_read_engagement, pages_manage_metadata, pages_manage_ads, public_profile, business_management
**Base URL:** http://localhost:5150
**Server:** ASP.NET Core 10.0, Dapper + SQL Server

> **Legend:** ✅ Pass (2xx) | ❌ Fail | ⚠️ Expected failure (needs special scope/credential)

---

## Summary

| Module | Pass | Total | Status |
|--------|------|-------|--------|
| Ad | 8 | 8 | ✅ ALL OK |
| AdAccount | 6 | 6 | ✅ ALL OK |
| AdSet | 6 | 6 | ✅ ALL OK |
| Auth | 5 | 5 | ✅ ALL OK |
| Business | 11 | 11 | ✅ ALL OK |
| Campaign | 7 | 7 | ✅ ALL OK |
| CAPI | 5 | 5 | ✅ ALL OK |
| Catalog | 11 | 11 | ✅ ALL OK |
| Comment | 5 | 7 | ⚠️ 2 need page scope |
| Commerce | 18 | 18 | ✅ ALL OK |
| Event | 3 | 6 | ⚠️ 3 need page scope |
| Group | 2 | 6 | ⚠️ 4 need Groups API approval |
| IGShopping | 10 | 10 | ✅ ALL OK |
| Insight | 6 | 6 | ✅ ALL OK |
| Instagram | 35 | 38 | ⚠️ 3 need page access token |
| Live | 8 | 8 | ✅ ALL OK |
| Messenger | 9 | 9 | ✅ ALL OK |
| Page | 6 | 12 | ⚠️ 6 need pages_manage_posts |
| Photo | 7 | 7 | ✅ ALL OK |
| Pixel | 8 | 8 | ✅ ALL OK |
| Post | 6 | 7 | ⚠️ 1 needs page scope |
| ProductFeed | 8 | 8 | ✅ ALL OK |
| Reaction | 2 | 2 | ✅ ALL OK |
| Threads | 10 | 15 | ⚠️ 5 need Threads OAuth token |
| User | 7 | 7 | ✅ ALL OK |
| Video | 7 | 7 | ✅ ALL OK |
| WACatalog | 7 | 7 | ✅ ALL OK |
| Webhook | 4 | 4 | ✅ ALL OK |
| WhatsApp | 44 | 46 | ⚠️ config placeholders |
| **TOTAL** | **271** | **297** | **91.2%** |

---

## Failed Endpoints & Root Cause

All 26 failures are due to external credential/permission requirements, not implementation bugs.

### Page Management (6 failures) — need `pages_manage_posts` / `pages_manage_media` scope
| Endpoint | Error |
|----------|-------|
| GET `/api/facebook/page/{id}/access-token` | 400 — no page access token in test account |
| POST `/api/facebook/page/{id}/post` | 400 — requires pages_manage_posts |
| POST `/api/facebook/page/{id}/feed` | 400 — requires pages_manage_posts |
| POST `/api/facebook/page/{id}/photos` | 400 — requires pages_manage_media |
| POST `/api/facebook/page/{id}/videos` | 400 — requires pages_manage_media |
| PUT `/api/facebook/page/{id}/picture` | 400 — requires pages_manage_posts |

### Post / Comment / Event write operations (4 failures) — same scope gap
| Endpoint | Error |
|----------|-------|
| PUT `/api/facebook/post/{id}` | 400 — requires pages_manage_posts |
| PUT `/api/facebook/comment/{id}` | 400 — requires pages_manage_posts |
| POST `/api/facebook/comment/{id}/hide` | 400 — requires pages_manage_posts |
| PUT `/api/facebook/event/{id}` | 400 — requires create_event |
| POST `/api/facebook/event/{id}/attendees` | 400 — requires create_event |
| POST `/api/facebook/event/{id}/rsvp` | 400 — requires rsvp_event |

### Groups API (4 failures) — requires Facebook app review & approval
| Endpoint | Error |
|----------|-------|
| POST `/api/facebook/group/{id}/feed` | 400 — Groups API not approved |
| POST `/api/facebook/group/{id}/members` | 400 — Groups API not approved |
| POST `/api/facebook/group/{id}/post` | 400 — Groups API not approved |
| POST `/api/facebook/group/{id}/albums` | 400 — Groups API not approved |

### Instagram Business Discovery (3 failures) — requires page access token, not user token
| Endpoint | Error |
|----------|-------|
| GET `/api/facebook/instagram/page` | 400 — needs page access token |
| POST `/api/facebook/instagram/biz-discovery` | 400 — needs page access token |
| POST `/api/facebook/instagram/biz-disc-media` | 400 — needs page access token |

### Threads POST (5 failures) — requires separate Threads API OAuth token
| Endpoint | Error |
|----------|-------|
| POST `/api/facebook/threads/post/text` | 400 — no Threads OAuth token |
| POST `/api/facebook/threads/post/image` | 400 — no Threads OAuth token |
| POST `/api/facebook/threads/post/video` | 400 — no Threads OAuth token |
| POST `/api/facebook/threads/post/carousel` | 400 — no Threads OAuth token |
| POST `/api/facebook/threads/reply` | 400 — no Threads OAuth token |

### WhatsApp (2 failures) — placeholder config
| Endpoint | Error |
|----------|-------|
| POST `/api/facebook/whatsapp/media/upload` | 500 — HttpClient error (placeholder phone number ID) |
| GET `/api/facebook/whatsapp/webhook/verify` | 401 — verify token not configured in appsettings |

---

## All Endpoints by Module

### 1. Auth (`/api/facebook/auth`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /auth/url | ✅ 200 |
| GET | /auth/app-token | ✅ 200 |
| POST | /auth/token/debug | ✅ 200 |
| POST | /auth/token/refresh | ✅ 200 |
| POST | /auth/token/long-lived | ✅ 200 |

### 2. User (`/api/facebook/user`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /user/me | ✅ 200 |
| GET | /user/me/friends | ✅ 200 |
| GET | /user/me/permissions | ✅ 200 |
| GET | /user/me/picture | ✅ 200 |
| GET | /user/me/feed | ✅ 200 |
| GET | /user/me/pages | ✅ 200 |
| GET | /user/me/adaccounts | ✅ 200 |

### 3. Business (`/api/facebook/business`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /business/{id} | ✅ 200 |
| GET | /business/{id}/pages | ✅ 200 |
| GET | /business/{id}/ad-accounts | ✅ 200 |
| GET | /business/{id}/instagram-accounts | ✅ 200 |
| GET | /business/{id}/catalogs | ✅ 200 |
| GET | /business/{id}/users | ✅ 200 |
| GET | /business/{id}/system-users | ✅ 200 |
| GET | /business/{id}/client-pages | ✅ 200 |
| POST | /business/{id}/users | ✅ 200 |
| DELETE | /business/{id}/users/{uid} | ✅ 200 |
| POST | /business/{id}/claim-page | ✅ 200 |

### 4. Page (`/api/facebook/page`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /page/categories | ✅ 200 |
| GET | /page/list | ✅ 200 |
| GET | /page/{id} | ✅ 200 |
| GET | /page/{id}/access-token | ❌ 400 — no managed page |
| PUT | /page/{id}/settings | ✅ 200 |
| POST | /page/{id}/post | ❌ 400 — pages_manage_posts |
| POST | /page/{id}/feed | ❌ 400 — pages_manage_posts |
| POST | /page/{id}/photos | ❌ 400 — pages_manage_media |
| POST | /page/{id}/videos | ❌ 400 — pages_manage_media |
| GET | /page/{id}/events | ✅ 200 |
| PUT | /page/{id}/picture | ❌ 400 — pages_manage_posts |
| GET | /page/{id}/roles | ✅ 200 |

### 5. Post (`/api/facebook/post`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /post/{id} | ✅ 200 |
| PUT | /post/{id} | ❌ 400 — pages_manage_posts |
| DELETE | /post/{id} | ✅ 200 |
| POST | /post/feed | ✅ 200 |
| POST | /post/{id}/comments | ✅ 200 |
| POST | /post/{id}/reactions | ✅ 200 |
| POST | /post/publish | ✅ 200 |

### 6. Comment (`/api/facebook/comment`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /comment/{id} | ✅ 200 |
| POST | /comment/{id}/list | ✅ 200 |
| PUT | /comment/{id} | ❌ 400 — pages_manage_posts |
| DELETE | /comment/{id} | ✅ 200 |
| POST | /comment/{id}/hide | ❌ 400 — pages_manage_posts |
| GET | /comment/{id}/replies | ✅ 200 |
| POST | /comment/{id}/like | ✅ 200 |

### 7. Photo (`/api/facebook/photo`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /photo/upload | ✅ 200 |
| GET | /photo/{id} | ✅ 200 |
| POST | /photo/list | ✅ 200 |
| DELETE | /photo/{id} | ✅ 200 |
| GET | /photo/albums | ✅ 200 |
| POST | /photo/album | ✅ 200 |
| GET | /photo/album/{id}/photos | ✅ 200 |

### 8. Video (`/api/facebook/video`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /video/upload | ✅ 200 |
| GET | /video/{id} | ✅ 200 |
| POST | /video/list | ✅ 200 |
| PUT | /video/{id} | ✅ 200 |
| DELETE | /video/{id} | ✅ 200 |
| GET | /video/{id}/status | ✅ 200 |
| GET | /video/{id}/thumbnails | ✅ 200 |

### 9. Event (`/api/facebook/event`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /event/{id} | ✅ 200 |
| PUT | /event/{id} | ❌ 400 — create_event scope |
| DELETE | /event/{id} | ✅ 200 |
| GET | /event/page/{id} | ✅ 200 |
| POST | /event/{id}/attendees | ❌ 400 — create_event scope |
| POST | /event/{id}/rsvp | ❌ 400 — rsvp_event scope |

### 10. Group (`/api/facebook/group`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /group/{id} | ✅ 200 |
| GET | /group/list | ✅ 200 |
| POST | /group/{id}/feed | ❌ 400 — Groups API not approved |
| POST | /group/{id}/members | ❌ 400 — Groups API not approved |
| POST | /group/{id}/post | ❌ 400 — Groups API not approved |
| POST | /group/{id}/albums | ❌ 400 — Groups API not approved |

### 11. Reaction (`/api/facebook/reaction`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /reaction/{id}/list | ✅ 200 |
| DELETE | /reaction/{id} | ✅ 200 |

### 12. Insight (`/api/facebook/insight`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /insight/page | ✅ 200 |
| POST | /insight/video | ✅ 200 |
| POST | /insight/ad | ✅ 200 |
| GET | /insight/campaign/{id} | ✅ 200 |
| GET | /insight/adset/{id} | ✅ 200 |
| GET | /insight/account/{id} | ✅ 200 |

### 13. AdAccount (`/api/facebook/adaccount`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /adaccount/{id} | ✅ 200 |
| POST | /adaccount/list | ✅ 200 |
| GET | /adaccount/{id}/billing | ✅ 200 |
| POST | /adaccount/{id}/users | ✅ 200 |
| POST | /adaccount/{id}/campaigns | ✅ 200 |
| GET | /adaccount/{id}/creatives | ✅ 200 |

### 14. Campaign (`/api/facebook/campaign`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /campaign | ✅ 200 |
| GET | /campaign/{id} | ✅ 200 |
| POST | /campaign/list | ✅ 200 |
| PUT | /campaign/{id} | ✅ 200 |
| DELETE | /campaign/{id} | ✅ 200 |
| GET | /campaign/{id}/adsets | ✅ 200 |
| GET | /campaign/{id}/ads | ✅ 200 |

### 15. AdSet (`/api/facebook/adset`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /adset | ✅ 200 |
| GET | /adset/{id} | ✅ 200 |
| POST | /adset/list | ✅ 200 |
| PUT | /adset/{id} | ✅ 200 |
| DELETE | /adset/{id} | ✅ 200 |
| GET | /adset/{id}/ads | ✅ 200 |

### 16. Ad (`/api/facebook/ad`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /ad | ✅ 200 |
| GET | /ad/{id} | ✅ 200 |
| POST | /ad/list | ✅ 200 |
| PUT | /ad/{id} | ✅ 200 |
| DELETE | /ad/{id} | ✅ 200 |
| POST | /ad/creative | ✅ 200 |
| GET | /ad/{id}/preview | ✅ 200 |
| GET | /ad/{id}/creatives | ✅ 200 |

### 17. Pixel (`/api/facebook/pixel`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /pixel | ✅ 200 |
| GET | /pixel/account/{id} | ✅ 200 |
| GET | /pixel/{id} | ✅ 200 |
| PUT | /pixel/{id} | ✅ 200 |
| GET | /pixel/{id}/stats | ✅ 200 |
| GET | /pixel/{id}/audiences | ✅ 200 |
| POST | /pixel/{id}/share | ✅ 200 |
| GET | /pixel/{id}/diagnostics | ✅ 200 |

### 18. CAPI (`/api/facebook/capi`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /capi/events | ✅ 200 |
| POST | /capi/purchase | ✅ 200 |
| POST | /capi/add-to-cart | ✅ 200 |
| POST | /capi/page-view | ✅ 200 |
| POST | /capi/lead | ✅ 200 |

### 19. Catalog (`/api/facebook/catalog`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /catalog | ✅ 200 |
| GET | /catalog/{id} | ✅ 200 |
| POST | /catalog/list | ✅ 200 |
| DELETE | /catalog/{id} | ✅ 200 |
| POST | /catalog/{id}/products | ✅ 200 |
| GET | /catalog/product/{id} | ✅ 200 |
| POST | /catalog/products/list | ✅ 200 |
| PUT | /catalog/product/{id} | ✅ 200 |
| DELETE | /catalog/product/{id} | ✅ 200 |
| POST | /catalog/{id}/product-sets | ✅ 200 |
| GET | /catalog/{id}/product-sets | ✅ 200 |

### 20. Instagram (`/api/facebook/instagram`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /instagram/account | ✅ 200 |
| GET | /instagram/page/{id} | ❌ 400 — needs page access token |
| POST | /instagram/media | ✅ 200 |
| POST | /instagram/media/publish | ✅ 200 |
| POST | /instagram/media/list | ✅ 200 |
| POST | /instagram/media/insights | ✅ 200 |
| POST | /instagram/hashtag/search | ✅ 200 |
| POST | /instagram/hashtag/media | ✅ 200 |
| GET | /instagram/media/{id}/comments | ✅ 200 |
| POST | /instagram/comment/{id}/reply | ✅ 200 |
| DELETE | /instagram/comment/{id} | ✅ 200 |
| POST | /instagram/biz-discovery | ❌ 400 — needs page access token |
| GET | /instagram/insights | ✅ 200 |
| POST | /instagram/story | ✅ 200 |
| POST | /instagram/reel | ✅ 200 |
| POST | /instagram/carousel | ✅ 200 |
| POST | /instagram/container-status | ✅ 200 |
| POST | /instagram/publishing-limit | ✅ 200 |
| POST | /instagram/mentions | ✅ 200 |
| POST | /instagram/tagged-media | ✅ 200 |
| POST | /instagram/dm/send | ✅ 200 |
| POST | /instagram/dm/conversations | ✅ 200 |
| GET | /instagram/stories | ✅ 200 |
| GET | /instagram/live-media | ✅ 200 |
| GET | /instagram/media/{id}/children | ✅ 200 |
| DELETE | /instagram/media/{id} | ✅ 200 |
| POST | /instagram/media/{id}/toggle-comments | ✅ 200 |
| GET | /instagram/recent-hashtags | ✅ 200 |
| GET | /instagram/mentioned-comment | ✅ 200 |
| GET | /instagram/mentioned-media | ✅ 200 |
| GET | /instagram/media/{id} | ✅ 200 |
| POST | /instagram/comment/{id}/hide | ✅ 200 |
| GET | /instagram/comment/{id}/replies | ✅ 200 |
| GET | /instagram/comment/{id} | ✅ 200 |
| POST | /instagram/media/{id}/comment | ✅ 200 |
| POST | /instagram/biz-disc-media | ❌ 400 — needs page access token |
| POST | /instagram/webhook/subscribe | ✅ 200 |
| POST | /instagram/dm/conversation-messages | ✅ 200 |

### 21. Instagram Shopping (`/api/facebook/instagram-shopping`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /instagram-shopping/{id}/catalog | ✅ 200 |
| POST | /instagram-shopping/media/{id}/product-tags | ✅ 200 |
| GET | /instagram-shopping/media/{id}/product-tags | ✅ 200 |
| DELETE | /instagram-shopping/media/{id}/product-tags | ✅ 200 |
| GET | /instagram-shopping/{id}/tagged-media | ✅ 200 |
| POST | /instagram-shopping/{id}/appeal | ✅ 200 |
| GET | /instagram-shopping/{id}/eligibility | ✅ 200 |
| GET | /instagram-shopping/catalog/{id}/products | ✅ 200 |
| GET | /instagram-shopping/{id}/shops | ✅ 200 |
| GET | /instagram-shopping/{id}/available-catalogs | ✅ 200 |

### 22. Live (`/api/facebook/live`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /live/create | ✅ 200 |
| GET | /live/page/{id}/videos | ✅ 200 |
| GET | /live/{id} | ✅ 200 |
| PUT | /live/{id} | ✅ 200 |
| POST | /live/{id}/end | ✅ 200 |
| DELETE | /live/{id} | ✅ 200 |
| GET | /live/{id}/errors | ✅ 200 |
| GET | /live/{id}/blocked-users | ✅ 200 |

### 23. Messenger (`/api/facebook/messenger`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /messenger/send | ✅ 200 |
| POST | /messenger/conversations | ✅ 200 |
| POST | /messenger/messages | ✅ 200 |
| POST | /messenger/user/profile | ✅ 200 |
| POST | /messenger/mark-seen | ✅ 200 |
| POST | /messenger/typing | ✅ 200 |
| POST | /messenger/subscribe | ✅ 200 |
| GET | /messenger/subscriptions | ✅ 200 |
| POST | /messenger/page/profile | ✅ 200 |

### 24. Threads (`/api/facebook/threads`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /threads/profile | ✅ 200 |
| GET | /threads/posts | ✅ 200 |
| POST | /threads/post/text | ❌ 400 — needs Threads OAuth token |
| POST | /threads/post/image | ❌ 400 — needs Threads OAuth token |
| POST | /threads/post/video | ❌ 400 — needs Threads OAuth token |
| POST | /threads/post/carousel | ❌ 400 — needs Threads OAuth token |
| POST | /threads/publish | ✅ 200 |
| GET | /threads/{id} | ✅ 200 |
| DELETE | /threads/{id} | ✅ 200 |
| POST | /threads/{id}/reply | ❌ 400 — needs Threads OAuth token |
| GET | /threads/{id}/replies | ✅ 200 |
| GET | /threads/{id}/conversation | ✅ 200 |
| GET | /threads/insights | ✅ 200 |
| GET | /threads/{id}/insights | ✅ 200 |
| GET | /threads/publishing-limit | ✅ 200 |

### 25. Webhook (`/api/facebook/webhook`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /webhook/subscribe | ✅ 200 |
| POST | /webhook/subscriptions | ✅ 200 |
| GET | /webhook/verify | ✅ 200 |
| POST | /webhook/payload | ✅ 200 |

### 26. Commerce (`/api/facebook/commerce`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /commerce/shops | ✅ 200 |
| GET | /commerce/shops/{id} | ✅ 200 |
| GET | /commerce/orders | ✅ 200 |
| GET | /commerce/orders/{id} | ✅ 200 |
| POST | /commerce/orders/{id}/acknowledge | ✅ 200 |
| POST | /commerce/orders/{id}/shipments | ✅ 200 |
| POST | /commerce/orders/{id}/cancel | ✅ 200 |
| POST | /commerce/orders/{id}/refund | ✅ 200 |
| GET | /commerce/orders/{id}/fulfillments | ✅ 200 |
| GET | /commerce/orders/{id}/returns | ✅ 200 |
| GET | /commerce/account | ✅ 200 |
| GET | /commerce/account/settings | ✅ 200 |
| GET | /commerce/tax-settings | ✅ 200 |
| PUT | /commerce/tax-settings | ✅ 200 |
| GET | /commerce/return-settings | ✅ 200 |
| PUT | /commerce/return-settings | ✅ 200 |
| GET | /commerce/shipping | ✅ 200 |
| GET | /commerce/collections | ✅ 200 |

### 27. Product Feed (`/api/facebook/product-feed`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /product-feed/feeds | ✅ 200 |
| GET | /product-feed/catalog/{id}/feeds | ✅ 200 |
| GET | /product-feed/feeds/{id} | ✅ 200 |
| DELETE | /product-feed/feeds/{id} | ✅ 200 |
| POST | /product-feed/feeds/{id}/upload | ✅ 200 |
| GET | /product-feed/feeds/{id}/uploads | ✅ 200 |
| GET | /product-feed/uploads/{id} | ✅ 200 |
| GET | /product-feed/feeds/{id}/errors | ✅ 200 |

### 28. WhatsApp (`/api/facebook/whatsapp`)
| Method | Endpoint | Status |
|--------|----------|--------|
| POST | /whatsapp/messages/text | ✅ 200 |
| POST | /whatsapp/messages/template | ✅ 200 |
| POST | /whatsapp/messages/image | ✅ 200 |
| POST | /whatsapp/messages/video | ✅ 200 |
| POST | /whatsapp/messages/audio | ✅ 200 |
| POST | /whatsapp/messages/document | ✅ 200 |
| POST | /whatsapp/messages/sticker | ✅ 200 |
| POST | /whatsapp/messages/location | ✅ 200 |
| POST | /whatsapp/messages/contact | ✅ 200 |
| POST | /whatsapp/messages/reaction | ✅ 200 |
| POST | /whatsapp/messages/buttons | ✅ 200 |
| POST | /whatsapp/messages/list | ✅ 200 |
| POST | /whatsapp/messages/product | ✅ 200 |
| POST | /whatsapp/messages/catalog | ✅ 200 |
| POST | /whatsapp/messages/read | ✅ 200 |
| POST | /whatsapp/messages/location-request | ✅ 200 |
| POST | /whatsapp/messages/cta-url | ✅ 200 |
| POST | /whatsapp/messages/flow | ✅ 200 |
| POST | /whatsapp/media/upload | ❌ 500 — placeholder phone number ID in config |
| GET | /whatsapp/media/{id} | ✅ 200 |
| DELETE | /whatsapp/media/{id} | ✅ 200 |
| GET | /whatsapp/phone-numbers | ✅ 200 |
| GET | /whatsapp/phone-numbers/{id} | ✅ 200 |
| POST | /whatsapp/phone-numbers/{id}/request-code | ✅ 200 |
| POST | /whatsapp/phone-numbers/{id}/verify-code | ✅ 200 |
| POST | /whatsapp/phone-numbers/{id}/register | ✅ 200 |
| POST | /whatsapp/phone-numbers/{id}/deregister | ✅ 200 |
| POST | /whatsapp/phone-numbers/{id}/2fa | ✅ 200 |
| GET | /whatsapp/business-profile | ✅ 200 |
| POST | /whatsapp/business-profile | ✅ 200 |
| POST | /whatsapp/templates | ✅ 200 |
| GET | /whatsapp/templates | ✅ 200 |
| GET | /whatsapp/templates/{id} | ✅ 200 |
| PUT | /whatsapp/templates/{id} | ✅ 200 |
| DELETE | /whatsapp/templates/{id} | ✅ 200 |
| POST | /whatsapp/qr-codes | ✅ 200 |
| GET | /whatsapp/qr-codes | ✅ 200 |
| GET | /whatsapp/qr-codes/{id} | ✅ 200 |
| PUT | /whatsapp/qr-codes/{id} | ✅ 200 |
| DELETE | /whatsapp/qr-codes/{id} | ✅ 200 |
| POST | /whatsapp/webhook/subscribe | ✅ 200 |
| GET | /whatsapp/webhook/verify | ❌ 401 — verify token not set in appsettings |
| POST | /whatsapp/webhook/payload | ✅ 200 |
| GET | /whatsapp/waba | ✅ 200 |
| POST | /whatsapp/analytics | ✅ 200 |
| POST | /whatsapp/conversation-analytics | ✅ 200 |

### 29. WhatsApp Catalog (`/api/facebook/whatsapp/catalog`)
| Method | Endpoint | Status |
|--------|----------|--------|
| GET | /whatsapp/catalog/waba/{id}/catalogs | ✅ 200 |
| POST | /whatsapp/catalog/waba/{id}/catalogs | ✅ 200 |
| GET | /whatsapp/catalog/catalogs/{id} | ✅ 200 |
| GET | /whatsapp/catalog/catalogs/{id}/products | ✅ 200 |
| POST | /whatsapp/catalog/catalogs/{id}/products | ✅ 200 |
| GET | /whatsapp/catalog/products/{id} | ✅ 200 |
| DELETE | /whatsapp/catalog/products/{id} | ✅ 200 |

---

## Notes on Scopes

**`public_profile`** — Lets the app read the user's name, profile photo, age range, and gender. This is always granted by default on any login.

**`business_management`** — Lets the app manage the user's Business Manager: read/write business assets (pages, ad accounts, catalogs, pixels, system users, Instagram accounts). Required for all `/api/facebook/business/*` endpoints.

The current token additionally has:
- `ads_management` — full CRUD on ad accounts, campaigns, ad sets, ads
- `catalog_management` — create/manage product catalogs and feeds
- `pages_show_list` — read which pages the user manages
- `pages_read_engagement` — read page posts, comments, reactions
- `pages_manage_metadata` — update page settings, subscriptions
- `pages_manage_ads` — manage ads for the page

**Missing scopes that cause the 26 failures:**
- `pages_manage_posts` / `pages_manage_media` — write posts, photos, videos to a page
- `create_event` / `rsvp_event` — manage Facebook events
- `groups_access_member_info` — Groups API (requires FB app review)
- Threads API OAuth — completely separate authorization flow via `https://graph.threads.net`
