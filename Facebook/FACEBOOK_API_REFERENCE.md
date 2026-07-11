# Facebook ApiToolKit — Complete API Reference
**Base URL:** `http://localhost:5150`
**Graph API Version:** v21.0 (`https://graph.facebook.com/v21.0`)
**Total Endpoints:** 353 across 29 modules
**Last Updated:** 2026-07-10

---

## Genuineness Legend
- ✅ **Genuine** — Official Facebook/Meta Graph API endpoint, works with correct permissions
- ⚠️ **Restricted** — Genuine endpoint but requires special app review / advanced access
- ❌ **Not Supported** — Behaviour not available via public Graph API

---

## Testing Prerequisites Checklist

| Credential | Where to Get | Used By |
|---|---|---|
| **User Access Token** | OAuth flow → `/api/facebook/auth/auth-url` | All Graph API calls |
| **Page Access Token** | `/api/facebook/page/{pageId}/access-token` | Pages, Posts, Photos, Videos, Messenger |
| **App Access Token** | `/api/facebook/auth/app-token` | App-level operations |
| **Facebook User ID** | Token debug response | Users, Events, Groups |
| **Page ID** | Page URL or user pages list | Pages, Posts, Photos, Events |
| **IG User ID** | `/api/facebook/instagram/page/{pageId}/instagram` | All Instagram endpoints |
| **Ad Account ID** | Format: `act_XXXXXXXXXX` from Business Manager | All Ads/Campaign/Insight endpoints |
| **Business ID** | Business Manager URL | Business, Catalog, Pixel |
| **WhatsApp Phone Number ID** | Meta App → WhatsApp → API Setup | All WhatsApp endpoints |
| **WhatsApp WABA ID** | Meta App → WhatsApp → API Setup | Templates, analytics |
| **WhatsApp Access Token** | Meta Business → System Users | All WhatsApp endpoints |
| **Pixel ID** | Events Manager → Pixels | Pixel, CAPI endpoints |
| **Catalog ID** | Commerce Manager | Catalog, Product Feed, IG Shopping |
| **Threads Access Token** | Threads OAuth (graph.threads.net) | All Threads endpoints |

---

## Module 1 — Auth ✅
**Route:** `/api/facebook/auth` | **Graph API:** `oauth/access_token`, `/debug_token`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 1 | POST | `/auth-url` | Get OAuth login URL | `scope[]`, `state` |
| 2 | POST | `/token` | Exchange code for access token | `code`, `state` |
| 3 | GET | `/app-token` | Get app-level access token | _(uses config AppId/AppSecret)_ |
| 4 | POST | `/token/long-lived` | Exchange short-lived → 60-day token | `shortLivedToken` |
| 5 | POST | `/token/debug` | Inspect token (expiry, scopes, userId) | `inputToken`, `accessToken` |
| 6 | POST | `/token/refresh` | Refresh a long-lived token | `longLivedToken` |

**Test order:** #1 → open URL in browser → approve → #5 to verify token

---

## Module 2 — Users ✅
**Route:** `/api/facebook/user` | **Graph API:** `/{user-id}`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 7 | GET | `/{userId}/profile` | Get name, email, birthday, locale | `userId`, `accessToken` |
| 8 | GET | `/{userId}/friends` | Friends who also use this app | `userId`, `accessToken` |
| 9 | GET | `/{userId}/permissions` | Granted OAuth permissions | `userId`, `accessToken` |
| 10 | GET | `/{userId}/picture` | Profile picture URL | `userId`, `accessToken`, `type` |
| 11 | GET | `/{userId}/feed` | User's news feed | `userId`, `accessToken`, `limit` |
| 12 | GET | `/{userId}/pages` | Pages the user manages | `userId`, `accessToken` |
| 13 | GET | `/{userId}/ad-accounts` | Ad accounts linked to user | `userId`, `accessToken` |

> ⚠️ Friends list only returns mutual friends who authorized the same app.

---

## Module 3 — Pages ✅
**Route:** `/api/facebook/page` | **Graph API:** `/{page-id}`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 14 | GET | `/{pageId}/info` | Page name, category, about, website | `pageId`, `accessToken` |
| 15 | GET | `/user/{userId}/pages` | All pages managed by user | `userId`, `accessToken` |
| 16 | GET | `/{pageId}/access-token` | Page-specific access token | `pageId`, `accessToken` |
| 17 | PUT | `/{pageId}/settings` | Update page settings | `pageId`, settings object, `accessToken` |
| 18 | GET | `/categories` | List available page categories | `accessToken` |
| 19 | POST | `/{pageId}/post` | Create a post on the page | `pageId`, `message`, `accessToken` |
| 20 | POST | `/{pageId}/feed` | Get page's post feed | `pageId`, `accessToken`, `limit` |
| 21 | POST | `/{pageId}/photos` | Get photos on the page | `pageId`, `accessToken`, `limit` |
| 22 | POST | `/{pageId}/videos` | Get videos on the page | `pageId`, `accessToken`, `limit` |
| 23 | GET | `/{pageId}/events` | Get events for a page | `pageId`, `accessToken` |
| 24 | PUT | `/{pageId}/picture` | Update page profile picture | `pageId`, `pictureUrl`, `accessToken` |
| 25 | GET | `/{pageId}/roles` | Get page roles/admins | `pageId`, `accessToken` |

---

## Module 4 — Posts ✅
**Route:** `/api/facebook/post` | **Graph API:** `/{post-id}`, `/{object-id}/feed`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 26 | POST | `/` | Create a new post | `objectId`, `message`, `link`, `accessToken` |
| 27 | GET | `/{postId}` | Get post details | `postId`, `accessToken` |
| 28 | PUT | `/{postId}` | Update post message | `postId`, `message`, `accessToken` |
| 29 | DELETE | `/{postId}` | Delete a post | `postId`, `accessToken` |
| 30 | POST | `/feed` | Get feed for a page or user | `objectId`, `accessToken`, `limit` |
| 31 | POST | `/{postId}/comments` | Get comments on a post | `postId`, `accessToken`, `limit` |
| 32 | POST | `/{postId}/reactions` | Get reactions on a post | `postId`, `accessToken` |
| 33 | POST | `/{postId}/publish` | Publish a scheduled post | `postId`, `accessToken` |

---

## Module 5 — Photos ✅
**Route:** `/api/facebook/photo` | **Graph API:** `/{object-id}/photos`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 34 | POST | `/upload` | Upload photo (URL) | `objectId`, `url`, `caption`, `accessToken` |
| 35 | GET | `/{photoId}` | Get photo details | `photoId`, `accessToken` |
| 36 | POST | `/list` | Get photos for page/user | `objectId`, `accessToken`, `limit` |
| 37 | DELETE | `/{photoId}` | Delete a photo | `photoId`, `accessToken` |
| 38 | GET | `/{pageId}/albums` | Get albums for page/user | `pageId`, `accessToken` |
| 39 | POST | `/album` | Create a new album | `objectId`, `name`, `description`, `accessToken` |
| 40 | GET | `/{albumId}/photos` | Get photos in an album | `albumId`, `accessToken` |

---

## Module 6 — Videos ✅
**Route:** `/api/facebook/video` | **Graph API:** `/{object-id}/videos`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 41 | POST | `/upload` | Upload video from URL | `objectId`, `fileUrl`, `title`, `description`, `accessToken` |
| 42 | GET | `/{videoId}` | Get video details | `videoId`, `accessToken` |
| 43 | POST | `/list` | Get videos for page/user | `objectId`, `accessToken`, `limit` |
| 44 | PUT | `/{videoId}` | Update video title/description | `videoId`, `title`, `description`, `accessToken` |
| 45 | DELETE | `/{videoId}` | Delete a video | `videoId`, `accessToken` |
| 46 | GET | `/{videoId}/status` | Get video processing status | `videoId`, `accessToken` |
| 47 | GET | `/{videoId}/thumbnails` | Get thumbnail options | `videoId`, `accessToken` |

---

## Module 7 — Comments ✅
**Route:** `/api/facebook/comment` | **Graph API:** `/{object-id}/comments`, `/{comment-id}`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 48 | POST | `/` | Create a comment | `objectId`, `message`, `accessToken` |
| 49 | GET | `/{commentId}` | Get comment details | `commentId`, `accessToken` |
| 50 | POST | `/list` | Get all comments on an object | `objectId`, `accessToken`, `limit` |
| 51 | PUT | `/{commentId}` | Update comment message | `commentId`, `message`, `accessToken` |
| 52 | DELETE | `/{commentId}` | Delete a comment | `commentId`, `accessToken` |
| 53 | POST | `/{commentId}/hide` | Hide or unhide a comment | `commentId`, `isHidden`, `accessToken` |
| 54 | GET | `/{commentId}/replies` | Get replies to a comment | `commentId`, `accessToken`, `limit` |
| 55 | POST | `/{commentId}/like` | Like a comment | `commentId`, `accessToken` |

---

## Module 8 — Reactions ⚠️ Partial
**Route:** `/api/facebook/reaction` | **Graph API:** `/{object-id}/reactions`

| # | Method | Endpoint | Purpose | Request Fields | Status |
|---|---|---|---|---|---|
| 56 | POST | `/` | Add reaction to an object | `objectId`, `reactionType`, `accessToken` | ❌ Not supported |
| 57 | POST | `/list` | Get reactions on an object | `objectId`, `accessToken` | ✅ |
| 58 | DELETE | `/{objectId}` | Remove a reaction | `objectId`, `accessToken` | ❌ Not supported |

> ❌ Facebook Graph API does NOT allow adding or removing reactions programmatically. Only reading via `GET /{object-id}/reactions` is supported. Endpoints #56 and #58 will receive API errors from Facebook.

---

## Module 9 — Events ✅ (requires `create_event` permission)
**Route:** `/api/facebook/event` | **Graph API:** `/{page-id}/events`, `/{event-id}`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 59 | POST | `/` | Create a new event | `pageId`, `name`, `startTime`, `endTime`, `description`, `accessToken` |
| 60 | GET | `/{eventId}` | Get event details | `eventId`, `accessToken` |
| 61 | PUT | `/{eventId}` | Update event details | `eventId`, `name`, `startTime`, `accessToken` |
| 62 | DELETE | `/{eventId}` | Delete an event | `eventId`, `accessToken` |
| 63 | GET | `/{pageId}/events` | Get all events for a page | `pageId`, `accessToken` |
| 64 | POST | `/{eventId}/attendees` | Get attending users | `eventId`, `accessToken` |
| 65 | POST | `/{eventId}/rsvp` | RSVP (attending/maybe/declined) | `eventId`, `rsvpStatus`, `accessToken` |

---

## Module 10 — Groups ⚠️ Restricted
**Route:** `/api/facebook/group` | **Graph API:** `/{group-id}`

> ⚠️ Groups API requires special Facebook approval. Most operations are blocked for standard apps.

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 66 | GET | `/{groupId}` | Get group details | `groupId`, `accessToken` |
| 67 | GET | `/user/{userId}/groups` | Get groups a user belongs to | `userId`, `accessToken` |
| 68 | POST | `/{groupId}/feed` | Get group post feed | `groupId`, `accessToken`, `limit` |
| 69 | POST | `/{groupId}/members` | Get group members | `groupId`, `accessToken` |
| 70 | POST | `/{groupId}/post` | Post in a group | `groupId`, `message`, `accessToken` |
| 71 | POST | `/{groupId}/albums` | Get group albums | `groupId`, `accessToken` |

---

## Module 11 — Ad Accounts ✅
**Route:** `/api/facebook/ad-account` | **Graph API (Marketing):** `/act_{ad-account-id}`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 72 | GET | `/{accountId}` | Get ad account details | `accountId` (act_XXX), `accessToken` |
| 73 | POST | `/user/ad-accounts` | Get user's ad accounts | `userId`, `accessToken` |
| 74 | GET | `/{accountId}/billing` | Get billing info | `accountId`, `accessToken` |
| 75 | POST | `/{accountId}/users` | Get users on account | `accountId`, `accessToken` |
| 76 | POST | `/{accountId}/campaigns` | Get campaigns | `accountId`, `accessToken`, `limit` |
| 77 | GET | `/{accountId}/creatives` | Get ad creatives | `accountId`, `accessToken` |

---

## Module 12 — Campaigns ✅
**Route:** `/api/facebook/campaign` | **Graph API (Marketing):** `/{campaign-id}`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 78 | POST | `/` | Create campaign | `adAccountId`, `name`, `objective`, `status`, `accessToken` |
| 79 | GET | `/{campaignId}` | Get campaign details | `campaignId`, `accessToken` |
| 80 | POST | `/list` | Get campaigns for an account | `adAccountId`, `accessToken` |
| 81 | PUT | `/{campaignId}` | Update campaign | `campaignId`, `name`, `status`, `accessToken` |
| 82 | DELETE | `/{campaignId}` | Delete campaign | `campaignId`, `accessToken` |
| 83 | GET | `/{campaignId}/adsets` | Get ad sets in campaign | `campaignId`, `accessToken` |
| 84 | GET | `/{campaignId}/ads` | Get ads in campaign | `campaignId`, `accessToken` |

---

## Module 13 — Ad Sets ✅
**Route:** `/api/facebook/adset` | **Graph API (Marketing):** `/{adset-id}`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 85 | POST | `/` | Create ad set | `campaignId`, `name`, `dailyBudget`, `billingEvent`, `optimizationGoal`, `targetingSpec`, `accessToken` |
| 86 | GET | `/{adSetId}` | Get ad set details | `adSetId`, `accessToken` |
| 87 | POST | `/list` | Get ad sets for campaign | `campaignId`, `accessToken` |
| 88 | PUT | `/{adSetId}` | Update ad set | `adSetId`, `name`, `status`, `accessToken` |
| 89 | DELETE | `/{adSetId}` | Delete ad set | `adSetId`, `accessToken` |
| 90 | GET | `/{adSetId}/ads` | Get ads in ad set | `adSetId`, `accessToken` |

---

## Module 14 — Ads ✅
**Route:** `/api/facebook/ad` | **Graph API (Marketing):** `/{ad-id}`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 91 | POST | `/` | Create ad | `adSetId`, `name`, `creativeId`, `status`, `accessToken` |
| 92 | GET | `/{adId}` | Get ad details | `adId`, `accessToken` |
| 93 | POST | `/list` | Get ads for an ad set | `adSetId`, `accessToken` |
| 94 | PUT | `/{adId}` | Update ad | `adId`, `name`, `status`, `accessToken` |
| 95 | DELETE | `/{adId}` | Delete ad | `adId`, `accessToken` |
| 96 | POST | `/creative` | Create ad creative | `adAccountId`, `name`, `objectStorySpec`, `accessToken` |
| 97 | GET | `/{adId}/preview` | Preview an ad | `adId`, `adFormat`, `accessToken` |
| 98 | GET | `/account/{accountId}/creatives` | Get creatives for account | `accountId`, `accessToken` |

---

## Module 15 — Insights ✅
**Route:** `/api/facebook/insight` | **Graph API:** `/{object-id}/insights`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 99 | POST | `/page` | Page insights (reach, engagement) | `pageId`, `metrics[]`, `period`, `accessToken` |
| 100 | POST | `/video` | Video insights (views, retention) | `videoId`, `metrics[]`, `accessToken` |
| 101 | POST | `/ad` | Ad performance (impressions, clicks, spend) | `adId`, `fields[]`, `datePreset`, `accessToken` |
| 102 | GET | `/campaign/{campaignId}` | Campaign performance | `campaignId`, `accessToken` |
| 103 | GET | `/adset/{adSetId}` | Ad set performance | `adSetId`, `accessToken` |
| 104 | GET | `/account/{accountId}` | Account-level performance | `accountId`, `accessToken` |

---

## Module 16 — Messenger ✅
**Route:** `/api/facebook/messenger` | **Graph API:** `/{page-id}/messages`, `/me/conversations`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 105 | POST | `/send` | Send message via Messenger | `recipientId`, `messageText`, `pageAccessToken` |
| 106 | POST | `/conversations` | Get page conversations | `pageId`, `pageAccessToken` |
| 107 | POST | `/messages` | Get messages in conversation | `conversationId`, `pageAccessToken` |
| 108 | POST | `/user/profile` | Get user profile by PSID | `psid`, `pageAccessToken` |
| 109 | POST | `/mark-seen` | Mark conversation as seen | `recipientId`, `pageAccessToken` |
| 110 | POST | `/typing` | Send typing indicator | `recipientId`, `senderAction`, `pageAccessToken` |
| 111 | POST | `/subscribe` | Subscribe page to Messenger webhooks | `pageId`, `pageAccessToken` |
| 112 | GET | `/page/{pageId}/subscriptions` | Get webhook subscriptions | `pageId`, `pageAccessToken` |
| 113 | POST | `/page/{pageId}/profile` | Set Messenger profile (greeting, menu) | `pageId`, profile object, `pageAccessToken` |

---

## Module 17 — Instagram ✅ (38 endpoints)
**Route:** `/api/facebook/instagram` | **Graph API:** Instagram Graph API

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 114 | GET | `/{igUserId}/account` | Get IG account profile | `igUserId`, `accessToken` |
| 115 | GET | `/page/{pageId}/instagram` | Get IG account linked to FB page | `pageId`, `accessToken` |
| 116 | POST | `/media` | Create media container (image/video) | `igUserId`, `imageUrl`/`videoUrl`, `caption`, `mediaType`, `isAiGenerated`, `isPaidPartnership`, `altText`, `accessToken` |
| 117 | POST | `/media/publish` | Publish media container | `igUserId`, `creationId`, `accessToken` |
| 118 | POST | `/media/list` | List published media | `igUserId`, `limit`, `fields`, `accessToken` |
| 119 | POST | `/media/insights` | Get insights for a media object | `mediaId`, `metrics[]`, `accessToken` |
| 120 | POST | `/hashtag/search` | Search hashtag ID by name | `igUserId`, `hashtagName`, `accessToken` |
| 121 | POST | `/hashtag/media` | Get top/recent hashtag media | `hashtagId`, `igUserId`, `mediaType`, `accessToken` |
| 122 | GET | `/media/{mediaId}/comments` | Get comments on media | `mediaId`, `accessToken`, `limit` |
| 123 | POST | `/comment/{commentId}/reply` | Reply to a comment | `commentId`, `message`, `accessToken` |
| 124 | DELETE | `/comment/{commentId}` | Delete a comment | `commentId`, `accessToken` |
| 125 | POST | `/business-discovery` | Get public data of another IG business | `igUserId`, `username`, `accessToken` |
| 126 | GET | `/{igUserId}/insights` | Account-level insights (with date range) | `igUserId`, `metrics[]`, `period`, `since`, `until`, `accessToken` |
| 127 | POST | `/story` | Create Story container | `igUserId`, `imageUrl`/`videoUrl`, `accessToken` |
| 128 | POST | `/reel` | Create Reel container | `igUserId`, `videoUrl`, `caption`, `shareToFeed`, `coverUrl`, `accessToken` |
| 129 | POST | `/carousel` | Create Carousel container | `igUserId`, `items[]`, `caption`, `accessToken` |
| 130 | POST | `/container/status` | Check container processing status | `containerId`, `accessToken` |
| 131 | POST | `/publishing-limit` | Check daily publish quota (50/day) | `igUserId`, `accessToken` |
| 132 | POST | `/mentions` | Get media where account is @mentioned | `igUserId`, `limit`, `accessToken` |
| 133 | POST | `/tagged-media` | Get media where account is tagged | `igUserId`, `limit`, `accessToken` |
| 134 | POST | `/dm/send` | Send Instagram DM | `igUserId`, `recipientIgId`, `message`, `accessToken` |
| 135 | POST | `/dm/conversations` | Get DM conversations | `igUserId`, `accessToken` |
| 136 | GET | `/{igUserId}/stories` | Get active stories | `igUserId`, `accessToken`, `limit` |
| 137 | GET | `/{igUserId}/live-media` | Get live video broadcasts | `igUserId`, `accessToken` |
| 138 | GET | `/media/{mediaId}/children` | Get carousel album children | `mediaId`, `accessToken` |
| 139 | DELETE | `/media/{mediaId}` | Delete a media object | `mediaId`, `accessToken` |
| 140 | POST | `/media/{mediaId}/toggle-comments` | Enable/disable comments on media | `mediaId`, `commentsEnabled`, `accessToken` |
| 141 | GET | `/{igUserId}/recently-searched-hashtags` | Get recently searched hashtags (7 days) | `igUserId`, `accessToken` |
| 142 | GET | `/{igUserId}/mentioned-comment` | Get @mention comment details | `igUserId`, `commentId`, `accessToken` |
| 143 | GET | `/{igUserId}/mentioned-media` | Get @mention media details | `igUserId`, `mediaId`, `accessToken` |
| 144 | GET | `/media/{mediaId}/detail` | Get single media object by ID | `mediaId`, `fields`, `accessToken` |
| 145 | POST | `/comment/{commentId}/hide` | Hide or unhide a specific comment | `commentId`, `hide` (bool), `accessToken` |
| 146 | GET | `/comment/{commentId}/replies` | Get replies to a comment | `commentId`, `accessToken`, `limit` |
| 147 | GET | `/comment/{commentId}` | Get single comment with replies | `commentId`, `accessToken` |
| 148 | POST | `/media/{mediaId}/comment` | Add top-level comment to media | `mediaId`, `message`, `accessToken` |
| 149 | POST | `/business-discovery/media` | Get another account's published media | `igUserId`, `username`, `limit`, `accessToken` |
| 150 | POST | `/webhook/subscribe` | Subscribe to Instagram webhooks | `appId`, `callbackUrl`, `verifyToken`, `fields`, `accessToken` |
| 151 | POST | `/conversation/messages` | Get messages in a DM conversation | `conversationId`, `limit`, `accessToken` |

---

## Module 18 — Webhooks ✅
**Route:** `/api/facebook/webhook` | **Graph API:** `/{app-id}/subscriptions`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 152 | POST | `/subscribe` | Subscribe to webhook events | `appId`, `objectType`, `callbackUrl`, `verifyToken`, `fields[]`, `accessToken` |
| 153 | POST | `/subscriptions` | Get current subscriptions | `appId`, `accessToken` |
| 154 | DELETE | `/` | Delete a subscription | `appId`, `objectType`, `accessToken` |
| 155 | GET | `/verify` | Handle Facebook webhook challenge | _(auto-called by Facebook)_ |
| 156 | POST | `/payload` | Receive incoming webhook events | _(auto-called by Facebook)_ |

---

## Module 19 — WhatsApp Cloud API ✅ (46 endpoints)
**Route:** `/api/facebook/whatsapp` | **API:** `https://graph.facebook.com/v21.0`

### Messages
| # | Method | Endpoint | Purpose | Key Request Fields |
|---|---|---|---|---|
| 157 | POST | `/messages/text` | Send text message | `phoneNumberId`, `to`, `body`, `previewUrl`, `accessToken` |
| 158 | POST | `/messages/template` | Send approved template message | `phoneNumberId`, `to`, `templateName`, `languageCode`, `components[]`, `accessToken` |
| 159 | POST | `/messages/image` | Send image (URL or media ID) | `phoneNumberId`, `to`, `mediaId`/`link`, `caption`, `accessToken` |
| 160 | POST | `/messages/video` | Send video | `phoneNumberId`, `to`, `mediaId`/`link`, `caption`, `accessToken` |
| 161 | POST | `/messages/audio` | Send audio file | `phoneNumberId`, `to`, `mediaId`/`link`, `accessToken` |
| 162 | POST | `/messages/document` | Send document (PDF, DOCX) | `phoneNumberId`, `to`, `mediaId`/`link`, `filename`, `accessToken` |
| 163 | POST | `/messages/sticker` | Send sticker | `phoneNumberId`, `to`, `mediaId`/`link`, `accessToken` |
| 164 | POST | `/messages/location` | Send location pin | `phoneNumberId`, `to`, `latitude`, `longitude`, `name`, `address`, `accessToken` |
| 165 | POST | `/messages/contact` | Send contact card (vCard) | `phoneNumberId`, `to`, `firstName`, `lastName`, `phone`, `email`, `accessToken` |
| 166 | POST | `/messages/reaction` | React to a message with emoji | `phoneNumberId`, `to`, `messageId`, `emoji`, `accessToken` |
| 167 | POST | `/messages/interactive/buttons` | Send reply buttons (max 3) | `phoneNumberId`, `to`, `bodyText`, `buttons[]`, `headerText`, `footerText`, `accessToken` |
| 168 | POST | `/messages/interactive/list` | Send list menu with sections | `phoneNumberId`, `to`, `bodyText`, `sections[]`, `buttonText`, `accessToken` |
| 169 | POST | `/messages/interactive/product` | Send single product from catalog | `phoneNumberId`, `to`, `catalogId`, `productRetailerId`, `accessToken` |
| 170 | POST | `/messages/interactive/catalog` | Send multi-product catalog message | `phoneNumberId`, `to`, `catalogId`, `thumbnailProductRetailerId`, `accessToken` |
| 171 | POST | `/messages/interactive/location-request` | Request recipient's location | `phoneNumberId`, `to`, `bodyText`, `accessToken` |
| 172 | POST | `/messages/interactive/cta-url` | CTA button with external URL | `phoneNumberId`, `to`, `bodyText`, `displayText`, `url`, `headerText`, `accessToken` |
| 173 | POST | `/messages/interactive/flow` | Send WhatsApp Flow (interactive form) | `phoneNumberId`, `to`, `flowId`, `flowToken`, `flowCta`, `bodyText`, `accessToken` |
| 174 | POST | `/messages/read` | Mark a message as read | `phoneNumberId`, `messageId`, `accessToken` |

### Media
| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 175 | POST | `/media/upload` | Upload media to WhatsApp CDN | `phoneNumberId`, `fileUrl`, `mimeType`, `accessToken` |
| 176 | GET | `/media/{mediaId}` | Get uploaded media URL | `mediaId`, `accessToken` |
| 177 | DELETE | `/media/{mediaId}` | Delete uploaded media | `mediaId`, `accessToken` |

### Phone Numbers
| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 178 | GET | `/phone-numbers` | List all phone numbers | `wabaId`, `accessToken` |
| 179 | GET | `/phone-numbers/{phoneNumberId}` | Get phone number details | `phoneNumberId`, `accessToken` |
| 180 | POST | `/phone-numbers/{phoneNumberId}/request-code` | Request OTP (SMS or voice) | `phoneNumberId`, `codeMethod`, `language`, `accessToken` |
| 181 | POST | `/phone-numbers/{phoneNumberId}/verify-code` | Verify OTP code | `phoneNumberId`, `code`, `accessToken` |
| 182 | POST | `/phone-numbers/{phoneNumberId}/register` | Register phone with WA Platform | `phoneNumberId`, `pin`, `accessToken` |
| 183 | POST | `/phone-numbers/{phoneNumberId}/deregister` | Deregister phone | `phoneNumberId`, `accessToken` |
| 184 | POST | `/phone-numbers/{phoneNumberId}/2fa` | Set or update 2FA PIN | `phoneNumberId`, `pin`, `accessToken` |

### Business Profile
| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 185 | GET | `/business-profile` | Get WA business profile | `phoneNumberId`, `accessToken` |
| 186 | POST | `/business-profile` | Update profile (about, address, email) | `phoneNumberId`, `about`, `address`, `description`, `email`, `websites[]`, `vertical`, `accessToken` |

### Templates
| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 187 | POST | `/templates` | Create message template | `wabaId`, `name`, `category`, `language`, `components[]`, `accessToken` |
| 188 | GET | `/templates` | List all templates | `wabaId`, `accessToken` |
| 189 | GET | `/templates/{templateId}` | Get template by ID | `templateId`, `accessToken` |
| 190 | PUT | `/templates/{templateId}` | Update template components | `templateId`, `components[]`, `accessToken` |
| 191 | DELETE | `/templates/{templateId}` | Delete a template | `templateId`, `accessToken` |

### QR Codes
| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 192 | POST | `/qr-codes` | Create QR code with prefilled message | `phoneNumberId`, `prefilledMessage`, `imageFormat`, `accessToken` |
| 193 | GET | `/qr-codes` | List all QR codes | `phoneNumberId`, `accessToken` |
| 194 | GET | `/qr-codes/{qrCodeId}` | Get specific QR code | `phoneNumberId`, `qrCodeId`, `accessToken` |
| 195 | PUT | `/qr-codes/{qrCodeId}` | Update QR code message | `phoneNumberId`, `qrCodeId`, `prefilledMessage`, `accessToken` |
| 196 | DELETE | `/qr-codes/{qrCodeId}` | Delete a QR code | `phoneNumberId`, `qrCodeId`, `accessToken` |

### Webhooks + Analytics + WABA
| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 197 | POST | `/webhook/subscribe` | Subscribe to WA webhook events | `appId`, `callbackUrl`, `verifyToken`, `accessToken` |
| 198 | GET | `/webhook/verify` | Verify webhook challenge | _(auto from Facebook)_ |
| 199 | POST | `/webhook/payload` | Receive incoming WA webhook events | _(auto from Facebook)_ |
| 200 | GET | `/waba/{wabaId}` | Get WABA account info | `wabaId`, `accessToken` |
| 201 | POST | `/analytics` | Message analytics (sent/delivered/read) | `wabaId`, `start` (unix), `end` (unix), `granularity`, `accessToken` |
| 202 | POST | `/conversation-analytics` | Conversation cost breakdown | `wabaId`, `start` (unix), `end` (unix), `granularity`, `dimensions`, `accessToken` |

---

## Module 20 — Catalog ✅
**Route:** `/api/facebook/catalog` | **Graph API:** `/{catalog-id}`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 203 | POST | `/` | Create a catalog | `businessId`, `name`, `accessToken` |
| 204 | GET | `/{catalogId}` | Get catalog details | `catalogId`, `accessToken` |
| 205 | POST | `/list` | List catalogs for a business | `businessId`, `accessToken` |
| 206 | DELETE | `/{catalogId}` | Delete a catalog | `catalogId`, `accessToken` |
| 207 | POST | `/{catalogId}/product` | Add product to catalog | `catalogId`, product fields, `accessToken` |
| 208 | GET | `/product/{productId}` | Get product details | `productId`, `accessToken` |
| 209 | POST | `/{catalogId}/products` | List all products | `catalogId`, `accessToken`, `limit` |
| 210 | PUT | `/product/{productId}` | Update product | `productId`, product fields, `accessToken` |
| 211 | DELETE | `/product/{productId}` | Delete product | `productId`, `accessToken` |
| 212 | POST | `/{catalogId}/product-set` | Create product set | `catalogId`, `name`, `filter`, `accessToken` |
| 213 | GET | `/{catalogId}/product-sets` | List product sets | `catalogId`, `accessToken` |

---

## Module 21 — Threads ✅
**Route:** `/api/facebook/threads` | **API:** `https://graph.threads.net/v1.0`

> ⚠️ Requires **separate Threads OAuth** — different App and token from Facebook OAuth.

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 214 | GET | `/profile` | Get Threads user profile | `accessToken` |
| 215 | GET | `/posts` | List user's Threads posts | `accessToken`, `limit` |
| 216 | POST | `/posts/text` | Create text post | `text`, `accessToken` |
| 217 | POST | `/posts/image` | Create image post | `imageUrl`, `text`, `accessToken` |
| 218 | POST | `/posts/video` | Create video post | `videoUrl`, `text`, `accessToken` |
| 219 | POST | `/posts/carousel` | Create carousel post | `items[]`, `text`, `accessToken` |
| 220 | POST | `/posts/publish` | Publish a media container | `creationId`, `accessToken` |
| 221 | GET | `/posts/{threadId}` | Get a specific Threads post | `threadId`, `accessToken` |
| 222 | DELETE | `/posts/{threadId}` | Delete a post | `threadId`, `accessToken` |
| 223 | POST | `/posts/{threadId}/reply` | Reply to a post | `threadId`, `text`, `accessToken` |
| 224 | GET | `/posts/{threadId}/replies` | Get direct replies | `threadId`, `accessToken` |
| 225 | GET | `/posts/{threadId}/conversation` | Get full conversation thread | `threadId`, `accessToken` |
| 226 | GET | `/insights` | Account-level insights | `metrics[]`, `period`, `accessToken` |
| 227 | GET | `/posts/{threadId}/insights` | Post engagement insights | `threadId`, `metrics[]`, `accessToken` |
| 228 | GET | `/publishing-limit` | Get publishing rate limit quota | `accessToken` |

---

## Module 22 — Commerce / Shops ✅
**Route:** `/api/facebook/commerce` | **Graph API:** Commerce API

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 229 | GET | `/shops/{pageId}` | Get shops for a page | `pageId`, `accessToken` |
| 230 | GET | `/shops/detail/{shopId}` | Get shop details | `shopId`, `accessToken` |
| 231 | GET | `/orders/{pageId}` | Get orders (filter by state) | `pageId`, `state`, `accessToken` |
| 232 | GET | `/orders/detail/{orderId}` | Get order details | `orderId`, `accessToken` |
| 233 | POST | `/orders/{orderId}/acknowledge` | Acknowledge an order | `orderId`, `accessToken` |
| 234 | POST | `/orders/{orderId}/shipments` | Add shipment tracking | `orderId`, `trackingNumber`, `carrier`, `accessToken` |
| 235 | POST | `/orders/{orderId}/cancel` | Cancel an order | `orderId`, `cancelReason`, `accessToken` |
| 236 | POST | `/orders/{orderId}/refund` | Refund an order | `orderId`, `refundAmount`, `accessToken` |
| 237 | GET | `/orders/{orderId}/fulfillments` | Get order fulfillments | `orderId`, `accessToken` |
| 238 | GET | `/orders/{orderId}/returns` | Get order returns | `orderId`, `accessToken` |
| 239 | GET | `/account/{pageId}` | Get commerce account | `pageId`, `accessToken` |
| 240 | GET | `/account/{commerceAccountId}/settings` | Get commerce settings | `commerceAccountId`, `accessToken` |
| 241 | GET | `/account/{commerceAccountId}/tax-settings` | Get tax settings | `commerceAccountId`, `accessToken` |
| 242 | PUT | `/account/{commerceAccountId}/tax-settings` | Update tax settings | `commerceAccountId`, tax object, `accessToken` |
| 243 | GET | `/account/{commerceAccountId}/return-settings` | Get return settings | `commerceAccountId`, `accessToken` |
| 244 | PUT | `/account/{commerceAccountId}/return-settings` | Update return settings | `commerceAccountId`, return object, `accessToken` |
| 245 | GET | `/account/{commerceAccountId}/shipping-profiles` | Get shipping profiles | `commerceAccountId`, `accessToken` |
| 246 | GET | `/shops/{shopId}/collections` | Get shop collections | `shopId`, `accessToken` |

---

## Module 23 — Product Feed ✅
**Route:** `/api/facebook/product-feed` | **Graph API:** `/{catalog-id}/product_feeds`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 247 | POST | `/{catalogId}/feeds` | Create a product feed | `catalogId`, `name`, `schedule`, `accessToken` |
| 248 | GET | `/{catalogId}/feeds` | List feeds for catalog | `catalogId`, `accessToken` |
| 249 | GET | `/feeds/{feedId}` | Get feed details | `feedId`, `accessToken` |
| 250 | DELETE | `/feeds/{feedId}` | Delete a feed | `feedId`, `accessToken` |
| 251 | POST | `/feeds/{feedId}/upload` | Upload feed file (multipart) | `feedId`, file, `accessToken` |
| 252 | GET | `/feeds/{feedId}/uploads` | Get all uploads for feed | `feedId`, `accessToken` |
| 253 | GET | `/uploads/{uploadId}` | Get upload details | `uploadId`, `accessToken` |
| 254 | GET | `/feeds/{feedId}/errors` | Get feed validation errors | `feedId`, `accessToken` |

---

## Module 24 — Meta Pixel ✅
**Route:** `/api/facebook/pixel` | **Graph API (Marketing):** `/{pixel-id}`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 255 | POST | `/account/{adAccountId}` | Create a Pixel | `adAccountId`, `name`, `accessToken` |
| 256 | GET | `/account/{adAccountId}` | List Pixels for account | `adAccountId`, `accessToken` |
| 257 | GET | `/{pixelId}` | Get Pixel details | `pixelId`, `accessToken` |
| 258 | PUT | `/{pixelId}` | Update Pixel name | `pixelId`, `name`, `accessToken` |
| 259 | GET | `/{pixelId}/stats` | Get Pixel firing stats | `pixelId`, `accessToken` |
| 260 | GET | `/{pixelId}/audiences` | Get Pixel custom audiences | `pixelId`, `accessToken` |
| 261 | POST | `/{pixelId}/share` | Share Pixel with another account | `pixelId`, `accountId`, `accessToken` |
| 262 | GET | `/{pixelId}/diagnostics` | Get Pixel diagnostics | `pixelId`, `accessToken` |

---

## Module 25 — Conversions API (CAPI) ✅
**Route:** `/api/facebook/capi` | **Graph API:** `/{pixel-id}/events`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 263 | POST | `/events` | Send custom server-side events | `pixelId`, `events[]`, `accessToken` |
| 264 | POST | `/events/purchase` | Send Purchase conversion event | `pixelId`, `currency`, `value`, `email`, `accessToken` |
| 265 | POST | `/events/add-to-cart` | Send AddToCart event | `pixelId`, `currency`, `value`, `contentIds[]`, `accessToken` |
| 266 | POST | `/events/page-view` | Send PageView event | `pixelId`, `pageUrl`, `accessToken` |
| 267 | POST | `/events/lead` | Send Lead event | `pixelId`, `email`, `accessToken` |

---

## Module 26 — Instagram Shopping ✅
**Route:** `/api/facebook/instagram-shopping` | **Graph API:** Instagram Shopping

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 268 | GET | `/{igUserId}/catalog` | Get linked product catalog | `igUserId`, `accessToken` |
| 269 | POST | `/media/{mediaId}/product-tags` | Tag products on a media object | `mediaId`, `productTags[]`, `accessToken` |
| 270 | GET | `/media/{mediaId}/product-tags` | Get product tags on media | `mediaId`, `accessToken` |
| 271 | DELETE | `/media/{mediaId}/product-tags` | Remove all product tags | `mediaId`, `accessToken` |
| 272 | GET | `/{igUserId}/tagged-media` | Get media tagged with products | `igUserId`, `accessToken` |
| 273 | POST | `/{igUserId}/appeal` | Appeal a rejected product | `igUserId`, `appealReason`, `accessToken` |
| 274 | GET | `/{igUserId}/eligibility` | Check shopping eligibility | `igUserId`, `accessToken` |
| 275 | GET | `/catalog/{catalogId}/products` | Get catalog products | `catalogId`, `accessToken` |
| 276 | GET | `/{igUserId}/shops` | Get IG shops | `igUserId`, `accessToken` |
| 277 | GET | `/{igUserId}/available-catalogs` | Get available catalogs | `igUserId`, `accessToken` |

---

## Module 27 — Business Manager ✅
**Route:** `/api/facebook/business` | **Graph API:** `/{business-id}`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 278 | GET | `/{businessId}` | Get business account details | `businessId`, `accessToken` |
| 279 | GET | `/{businessId}/pages` | Get owned pages | `businessId`, `accessToken` |
| 280 | GET | `/{businessId}/ad-accounts` | Get owned ad accounts | `businessId`, `accessToken` |
| 281 | GET | `/{businessId}/instagram-accounts` | Get owned IG accounts | `businessId`, `accessToken` |
| 282 | GET | `/{businessId}/catalogs` | Get owned catalogs | `businessId`, `accessToken` |
| 283 | GET | `/{businessId}/users` | Get business users | `businessId`, `accessToken` |
| 284 | POST | `/{businessId}/users` | Invite user by email | `businessId`, `email`, `role`, `accessToken` |
| 285 | DELETE | `/{businessId}/users/{userId}` | Remove user from business | `businessId`, `userId`, `accessToken` |
| 286 | GET | `/{businessId}/system-users` | Get system users | `businessId`, `accessToken` |
| 287 | POST | `/{businessId}/system-users` | Create system user | `businessId`, `name`, `role`, `accessToken` |
| 288 | GET | `/{businessId}/client-pages` | Get client pages | `businessId`, `accessToken` |
| 289 | POST | `/{businessId}/claim-page` | Claim a page into business | `businessId`, `pageId`, `accessToken` |

---

## Module 28 — Live Video ✅
**Route:** `/api/facebook/live` | **Graph API:** `/{page-id}/live_videos`

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 290 | POST | `/{pageId}` | Start a live broadcast | `pageId`, `title`, `description`, `accessToken` |
| 291 | GET | `/{pageId}/videos` | Get live videos for a page | `pageId`, `accessToken` |
| 292 | GET | `/video/{liveVideoId}` | Get live video details | `liveVideoId`, `accessToken` |
| 293 | PUT | `/video/{liveVideoId}` | Update title/description/status | `liveVideoId`, `title`, `status`, `accessToken` |
| 294 | POST | `/video/{liveVideoId}/end` | End a live broadcast | `liveVideoId`, `accessToken` |
| 295 | DELETE | `/video/{liveVideoId}` | Delete a live video | `liveVideoId`, `accessToken` |
| 296 | GET | `/video/{liveVideoId}/errors` | Get stream errors | `liveVideoId`, `accessToken` |
| 297 | GET | `/video/{liveVideoId}/blocked-users` | Get blocked users | `liveVideoId`, `accessToken` |

---

## Module 29 — WhatsApp Catalog ✅
**Route:** `/api/facebook/whatsapp/catalog` | **API:** WhatsApp Business Platform

| # | Method | Endpoint | Purpose | Request Fields |
|---|---|---|---|---|
| 298 | GET | `/{wabaId}/catalogs` | List catalogs for a WABA | `wabaId`, `accessToken` |
| 299 | POST | `/{wabaId}/catalogs` | Create catalog for a WABA | `wabaId`, `name`, `accessToken` |
| 300 | GET | `/catalogs/{catalogId}` | Get catalog details | `catalogId`, `accessToken` |
| 301 | GET | `/catalogs/{catalogId}/products` | List catalog products | `catalogId`, `accessToken` |
| 302 | POST | `/catalogs/{catalogId}/products` | Add product to catalog | `catalogId`, product fields, `accessToken` |
| 303 | GET | `/products/{productId}` | Get product details | `productId`, `accessToken` |
| 304 | DELETE | `/products/{productId}` | Delete product | `productId`, `accessToken` |

---

## Genuineness Summary

| Module | Endpoints | Status | Notes |
|---|---|---|---|
| Auth | 6 | ✅ | Standard OAuth 2.0 |
| Users | 7 | ✅ | Friends returns only mutual app users |
| Pages | 12 | ✅ | Page access token required for writes |
| Posts | 8 | ✅ | |
| Photos | 7 | ✅ | |
| Videos | 7 | ✅ | |
| Comments | 8 | ✅ | |
| Reactions | 3 | ⚠️ | GET only ✅ · POST/DELETE reactions ❌ |
| Events | 7 | ✅ | `create_event` permission required |
| Groups | 6 | ⚠️ | Requires Groups API special approval |
| Ad Accounts | 6 | ✅ | Marketing API |
| Campaigns | 7 | ✅ | Marketing API |
| Ad Sets | 6 | ✅ | Marketing API |
| Ads | 8 | ✅ | Marketing API |
| Insights | 6 | ✅ | |
| Messenger | 9 | ✅ | Page subscription required |
| Instagram | 38 | ✅ | All match official Instagram Graph API |
| Webhooks | 5 | ✅ | |
| WhatsApp | 46 | ✅ | All match WA Cloud API |
| Catalog | 11 | ✅ | |
| Threads | 15 | ✅ | Separate Threads OAuth required |
| Commerce | 18 | ✅ | Requires commerce partner access |
| Product Feed | 8 | ✅ | |
| Pixel | 8 | ✅ | |
| CAPI | 5 | ✅ | |
| IG Shopping | 10 | ✅ | Requires shopping-eligible IG account |
| Business Manager | 12 | ✅ | Business Management API |
| Live Video | 8 | ✅ | |
| WA Catalog | 7 | ✅ | |
| **TOTAL** | **353** | | |

---

## What You Need Before Testing

### Phase 1 — Core Facebook (Modules 1–7, 16, 18) — START HERE
- [ ] Facebook App ID + Secret ✅ _(already configured)_
- [ ] Complete OAuth login → get User Access Token
- [ ] Your Facebook **User ID** _(returned in token debug)_
- [ ] A **Page ID** you manage _(from user pages list)_
- [ ] **Page Access Token** _(from `/api/facebook/page/{pageId}/access-token`)_

### Phase 2 — Instagram (Module 17)
- [ ] IG User ID _(from linked IG account API)_
- [ ] Scopes needed: `instagram_basic`, `instagram_content_publish`, `instagram_manage_comments`, `pages_read_engagement`

### Phase 3 — WhatsApp (Modules 19, 29)
- [ ] **WhatsApp Phone Number ID** _(Meta App → WhatsApp → API Setup)_
- [ ] **WhatsApp WABA ID** _(same panel)_
- [ ] **Permanent System User Access Token** _(Meta Business → System Users)_
- [ ] **Test recipient phone** _(added as recipient in API Setup)_

### Phase 4 — Ads / Business (Modules 11–15, 24, 25, 27)
- [ ] **Ad Account ID** (format: `act_XXXXXXXXXX`)
- [ ] **Business Manager ID**
- [ ] **Meta Pixel ID**
- [ ] Scopes: `ads_management`, `ads_read`, `business_management`

### Phase 5 — Commerce / Catalog (Modules 20–23, 26)
- [ ] **Catalog ID** _(Commerce Manager)_
- [ ] **Commerce Account ID** _(if testing orders/shops)_

### Phase 6 — Threads (Module 21)
- [ ] Separate **Threads App** in Meta for Developers
- [ ] **Threads User ID** + **Threads Access Token** _(from Threads OAuth)_

---

*Generated by ApiToolKit • http://localhost:5150 • http://localhost:5150/swagger*
