# ApiToolKit — Complete API Documentation

> **Coverage:** YouTube · Google Ads · Google Workspace · Google Maps · Google Analytics · Google Cloud AI · BigQuery · Google Search · Google Identity · Firebase · Flipkart  
> **Note:** Facebook / Meta APIs are documented in Section 5 as reference (not yet implemented in this project).  
> All base URLs, request/response formats reflect true, production-accurate facts.

---

## Table of Contents

1. [YouTube APIs](#1-youtube-apis)
2. [Google APIs](#2-google-apis)
   - 2.1 [Google Ads API](#21-google-ads-api)
   - 2.2 [Google Workspace — Gmail](#22-google-workspace--gmail)
   - 2.3 [Google Workspace — Drive](#23-google-workspace--drive)
   - 2.4 [Google Workspace — Calendar](#24-google-workspace--calendar)
   - 2.5 [Google Workspace — Sheets](#25-google-workspace--sheets)
   - 2.6 [Google Workspace — Docs](#26-google-workspace--docs)
   - 2.7 [Google Workspace — Slides](#27-google-workspace--slides)
   - 2.8 [Google Workspace — Meet](#28-google-workspace--meet)
   - 2.9 [Google Workspace — Chat](#29-google-workspace--chat)
   - 2.10 [Google Workspace — Forms](#210-google-workspace--forms)
   - 2.11 [Google Maps Platform](#211-google-maps-platform)
   - 2.12 [Google Analytics (GA4)](#212-google-analytics-ga4)
   - 2.13 [Google Cloud Vision AI](#213-google-cloud-vision-ai)
   - 2.14 [Google Cloud Speech-to-Text](#214-google-cloud-speech-to-text)
   - 2.15 [Google Cloud Natural Language](#215-google-cloud-natural-language)
   - 2.16 [Google Cloud Translation](#216-google-cloud-translation)
   - 2.17 [Google BigQuery](#217-google-bigquery)
   - 2.18 [Google Custom Search](#218-google-custom-search)
   - 2.19 [Google Knowledge Graph](#219-google-knowledge-graph)
   - 2.20 [Google Identity & People API](#220-google-identity--people-api)
   - 2.21 [Firebase Cloud Messaging (FCM)](#221-firebase-cloud-messaging-fcm)
   - 2.22 [Firebase Auth & Remote Config](#222-firebase-auth--remote-config)
3. [Flipkart Seller APIs](#3-flipkart-seller-apis)
4. [Authentication Reference](#4-authentication-reference)
5. [Facebook / Meta APIs (Reference)](#5-facebook--meta-apis-reference)

---

## 1. YouTube APIs

**Project path:** `Google\YouTube\`  
**Swagger UI:** `https://localhost:5002/swagger`  
**Auth:** OAuth 2.0 — scope `https://www.googleapis.com/auth/youtube`

---

### 1.1 YouTube Auth

| | |
|---|---|
| **Purpose** | Obtain and refresh OAuth 2.0 tokens for all YouTube API calls |
| **Google Base URL** | `https://accounts.google.com/o/oauth2/v2/auth` (authorize) · `https://oauth2.googleapis.com/token` (token) |
| **Our Endpoint** | `GET /api/youtube/auth/url` · `GET /api/youtube/auth/callback` · `POST /api/youtube/auth/token/refresh` |

**Request — Get Auth URL:**
```
GET /api/youtube/auth/url
```

**Response:**
```json
{
  "success": true,
  "data": {
    "authUrl": "https://accounts.google.com/o/oauth2/v2/auth?client_id=...&scope=https://www.googleapis.com/auth/youtube&response_type=code&access_type=offline&prompt=consent"
  }
}
```

**Request — Exchange Code:**
```
GET /api/youtube/auth/callback?code={authorizationCode}&redirectUri={uri}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "accessToken": "ya29.a0...",
    "refreshToken": "1//0g...",
    "expiresIn": 3600,
    "tokenType": "Bearer",
    "scope": "https://www.googleapis.com/auth/youtube"
  }
}
```

**Request — Refresh Token:**
```
POST /api/youtube/auth/token/refresh
Authorization: Bearer {accessToken}

{
  "refreshToken": "1//0g...",
  "userId": "user_123"
}
```

---

### 1.2 YouTube Channels

| | |
|---|---|
| **Purpose** | Retrieve channel info, subscriber count, video count, branding and update channel metadata |
| **Google Base URL** | `https://www.googleapis.com/youtube/v3/channels` |
| **Our Endpoint** | `GET /api/youtube/channels/me` · `GET /api/youtube/channels/{channelId}` · `PUT /api/youtube/channels` |

**Request — Get My Channels:**
```
GET /api/youtube/channels/me
Authorization: Bearer {accessToken}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "channels": [
      {
        "id": "UCxxxxxx",
        "title": "My Channel",
        "description": "...",
        "customUrl": "@handle",
        "publishedAt": "2020-01-15T10:00:00Z",
        "country": "US",
        "viewCount": 1500000,
        "subscriberCount": 50000,
        "videoCount": 120,
        "thumbnailUrl": "https://yt3.ggpht.com/..."
      }
    ],
    "nextPageToken": null,
    "totalResults": 1
  }
}
```

**Request — Update Channel:**
```
PUT /api/youtube/channels
Authorization: Bearer {accessToken}

{
  "channelId": "UCxxxxxx",
  "description": "Updated description",
  "defaultLanguage": "en",
  "country": "US",
  "keywords": "tech,programming,tutorials"
}
```

---

### 1.3 YouTube Videos

| | |
|---|---|
| **Purpose** | List, retrieve, update, delete, and rate videos. Manage video privacy, titles, descriptions, tags, and categories |
| **Google Base URL** | `https://www.googleapis.com/youtube/v3/videos` |
| **Our Endpoints** | `GET /api/youtube/videos/list` · `GET /api/youtube/videos/{videoId}` · `PUT /api/youtube/videos` · `DELETE /api/youtube/videos/{videoId}` · `POST /api/youtube/videos/rate` |

**Request — List Videos:**
```
GET /api/youtube/videos/list?channelId=UCxxxxxx&pageSize=25&order=date
Authorization: Bearer {accessToken}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "videos": [
      {
        "id": "dQw4w9WgXcQ",
        "title": "Never Gonna Give You Up",
        "description": "...",
        "channelId": "UCxxxxxx",
        "publishedAt": "2009-10-25T06:57:33Z",
        "duration": "PT3M33S",
        "viewCount": 1400000000,
        "likeCount": 15000000,
        "commentCount": 2800000,
        "status": "public",
        "thumbnailUrl": "https://i.ytimg.com/vi/dQw4w9WgXcQ/maxresdefault.jpg",
        "tags": ["music","rick astley"],
        "categoryId": "10",
        "liveBroadcastContent": "none"
      }
    ],
    "nextPageToken": "EAAiEgon...",
    "totalResults": 245,
    "resultsPerPage": 25
  }
}
```

**Request — Update Video:**
```
PUT /api/youtube/videos
Authorization: Bearer {accessToken}

{
  "videoId": "dQw4w9WgXcQ",
  "title": "New Title",
  "description": "New description",
  "tags": ["tag1", "tag2"],
  "categoryId": "22",
  "status": "public",
  "defaultLanguage": "en"
}
```

**Request — Rate Video:**
```
POST /api/youtube/videos/rate
Authorization: Bearer {accessToken}

{
  "videoId": "dQw4w9WgXcQ",
  "rating": "like"
}
```
> `rating` values: `like` · `dislike` · `none`

---

### 1.4 YouTube Playlists

| | |
|---|---|
| **Purpose** | Create, manage and delete playlists; control privacy (public/private/unlisted) and ordering |
| **Google Base URL** | `https://www.googleapis.com/youtube/v3/playlists` |
| **Our Endpoints** | `GET /api/youtube/playlists` · `GET /api/youtube/playlists/{id}` · `POST /api/youtube/playlists` · `PUT /api/youtube/playlists` · `DELETE /api/youtube/playlists/{id}` |

**Request — Create Playlist:**
```
POST /api/youtube/playlists
Authorization: Bearer {accessToken}

{
  "title": "My Favorites 2025",
  "description": "Hand-picked videos",
  "status": "public",
  "defaultLanguage": "en"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": "PLxxxxxx",
    "title": "My Favorites 2025",
    "channelId": "UCxxxxxx",
    "publishedAt": "2025-01-09T10:00:00Z",
    "itemCount": 0,
    "status": "public",
    "thumbnailUrl": null
  }
}
```

---

### 1.5 YouTube Playlist Items

| | |
|---|---|
| **Purpose** | Add, reorder, and remove videos from playlists |
| **Google Base URL** | `https://www.googleapis.com/youtube/v3/playlistItems` |
| **Our Endpoints** | `GET /api/youtube/playlist-items` · `POST /api/youtube/playlist-items` · `PUT /api/youtube/playlist-items` · `DELETE /api/youtube/playlist-items/{id}` |

**Request — Add to Playlist:**
```
POST /api/youtube/playlist-items
Authorization: Bearer {accessToken}

{
  "playlistId": "PLxxxxxx",
  "videoId": "dQw4w9WgXcQ",
  "position": 0
}
```

---

### 1.6 YouTube Comments

| | |
|---|---|
| **Purpose** | Read and moderate comments on videos; post new comments and replies; hide, approve, or reject comments |
| **Google Base URL** | `https://www.googleapis.com/youtube/v3/commentThreads` · `/comments` |
| **Our Endpoints** | `GET /api/youtube/comments/list` · `POST /api/youtube/comments/insert` · `POST /api/youtube/comments/reply` · `PUT /api/youtube/comments/update` · `DELETE /api/youtube/comments/{id}` · `POST /api/youtube/comments/moderate` |

**Request — List Comment Threads:**
```
GET /api/youtube/comments/list?videoId=dQw4w9WgXcQ&pageSize=100&order=time
Authorization: Bearer {accessToken}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "commentThreads": [
      {
        "id": "UgxZ...",
        "videoId": "dQw4w9WgXcQ",
        "totalReplyCount": 5,
        "canReply": true,
        "topLevelComment": {
          "id": "UgxZ...",
          "textDisplay": "This is a great video!",
          "authorDisplayName": "JohnDoe",
          "likeCount": 12,
          "publishedAt": "2025-01-05T14:22:00Z"
        }
      }
    ],
    "nextPageToken": "CAEQAA",
    "totalResults": 2800000
  }
}
```

**Request — Moderate Comment:**
```
POST /api/youtube/comments/moderate
Authorization: Bearer {accessToken}

{
  "commentId": "UgxZ...",
  "moderationStatus": "heldForReview"
}
```
> `moderationStatus` values: `published` · `heldForReview` · `rejected`

---

### 1.7 YouTube Search

| | |
|---|---|
| **Purpose** | Search YouTube for videos, channels, or playlists by keyword, date, duration, definition, type, and region |
| **Google Base URL** | `https://www.googleapis.com/youtube/v3/search` |
| **Our Endpoint** | `GET /api/youtube/search` |

**Request:**
```
GET /api/youtube/search?q=typescript+tutorial&type=video&pageSize=25&order=viewCount&videoDuration=medium
Authorization: Bearer {accessToken}
```

**Query Parameters:**
| Parameter | Type | Default | Description |
|---|---|---|---|
| `q` | string | required | Search query |
| `type` | string | `video` | `video` · `channel` · `playlist` |
| `pageSize` | int | 25 | 1–50 |
| `order` | string | `relevance` | `date` · `rating` · `relevance` · `title` · `viewCount` |
| `publishedAfter` | datetime | — | ISO 8601 |
| `channelId` | string | — | Restrict to channel |
| `videoDefinition` | string | — | `any` · `high` · `standard` |
| `videoDuration` | string | — | `any` · `long` (>20m) · `medium` (4–20m) · `short` (<4m) |

**Response:**
```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": { "kind": "youtube#video", "videoId": "abc123" },
        "snippet": {
          "publishedAt": "2024-06-01T12:00:00Z",
          "channelId": "UCxxxxxx",
          "title": "TypeScript Full Course",
          "description": "Learn TypeScript from zero...",
          "channelTitle": "Coding Academy",
          "thumbnailUrl": "https://i.ytimg.com/vi/abc123/mqdefault.jpg"
        }
      }
    ],
    "nextPageToken": "CBkQAA",
    "totalResults": 450000,
    "resultsPerPage": 25
  }
}
```

---

### 1.8 YouTube Subscriptions

| | |
|---|---|
| **Purpose** | List a user's channel subscriptions, subscribe to a channel, and unsubscribe |
| **Google Base URL** | `https://www.googleapis.com/youtube/v3/subscriptions` |
| **Our Endpoints** | `GET /api/youtube/subscriptions` · `POST /api/youtube/subscriptions` · `DELETE /api/youtube/subscriptions/{id}` |

---

### 1.9 YouTube Live Broadcasts

| | |
|---|---|
| **Purpose** | Create and manage YouTube live streams end-to-end: schedule, start testing, go live, and end broadcasts |
| **Google Base URL** | `https://www.googleapis.com/youtube/v3/liveBroadcasts` |
| **Our Endpoints** | `GET /api/youtube/live-broadcasts` · `POST /api/youtube/live-broadcasts` · `POST /api/youtube/live-broadcasts/{id}/transition` · `POST /api/youtube/live-broadcasts/{id}/bind` · `DELETE /api/youtube/live-broadcasts/{id}` |

**Request — Create Broadcast:**
```
POST /api/youtube/live-broadcasts
Authorization: Bearer {accessToken}

{
  "title": "Live Q&A Session",
  "description": "Monthly live session",
  "scheduledStartTime": "2025-02-01T18:00:00Z",
  "scheduledEndTime": "2025-02-01T20:00:00Z",
  "privacyStatus": "public",
  "enableAutoStart": false,
  "enableAutoStop": false
}
```

**Request — Transition Broadcast:**
```
POST /api/youtube/live-broadcasts/{broadcastId}/transition
Authorization: Bearer {accessToken}

{
  "broadcastId": "lbxxxxxx",
  "broadcastStatus": "live"
}
```
> `broadcastStatus` values: `testing` → `live` → `complete`

---

### 1.10 YouTube Live Streams

| | |
|---|---|
| **Purpose** | Create RTMP/HLS ingestion points (stream keys). A stream must be bound to a broadcast before going live |
| **Google Base URL** | `https://www.googleapis.com/youtube/v3/liveStreams` |
| **Our Endpoints** | `GET /api/youtube/live-streams` · `POST /api/youtube/live-streams` · `PUT /api/youtube/live-streams` · `DELETE /api/youtube/live-streams/{id}` |

**Request — Create Stream:**
```
POST /api/youtube/live-streams
Authorization: Bearer {accessToken}

{
  "title": "Main Stream Key",
  "format": "1080p",
  "ingestionType": "rtmp"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": "lsxxxxxx",
    "title": "Main Stream Key",
    "format": "1080p",
    "ingestionType": "rtmp",
    "ingestionAddress": "rtmp://a.rtmp.youtube.com/live2",
    "streamName": "xxxx-xxxx-xxxx-xxxx-xxxx",
    "backupIngestionAddress": "rtmp://b.rtmp.youtube.com/live2",
    "status": "active",
    "healthStatus": "good"
  }
}
```

---

### 1.11 YouTube Analytics

| | |
|---|---|
| **Purpose** | Query historical performance metrics: views, watch time, subscribers gained/lost, revenue, impressions, CTR by date, geography, device, traffic source, and more |
| **Google Base URL** | `https://youtubeanalytics.googleapis.com/v2/reports` |
| **Our Endpoint** | `POST /api/youtube/analytics/report` |

**Request:**
```
POST /api/youtube/analytics/report
Authorization: Bearer {accessToken}

{
  "ids": "channel==MINE",
  "startDate": "2024-01-01",
  "endDate": "2024-12-31",
  "metrics": "views,estimatedMinutesWatched,subscribersGained,subscribersLost",
  "dimensions": "day",
  "filters": "video==dQw4w9WgXcQ",
  "sort": "-views",
  "maxResults": 365
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "kind": "youtubeAnalytics#resultTable",
    "columnHeaders": [
      { "name": "day", "columnType": "DIMENSION", "dataType": "STRING" },
      { "name": "views", "columnType": "METRIC", "dataType": "INTEGER" }
    ],
    "rows": [
      ["2024-01-01", "15420"],
      ["2024-01-02", "18302"]
    ]
  }
}
```

---

### 1.12 YouTube Captions

| | |
|---|---|
| **Purpose** | Manage subtitle/caption tracks on videos: list available languages, upload SRT/VTT files, update draft status |
| **Google Base URL** | `https://www.googleapis.com/youtube/v3/captions` |
| **Our Endpoints** | `GET /api/youtube/captions` · `POST /api/youtube/captions` · `PUT /api/youtube/captions` · `DELETE /api/youtube/captions/{id}` |

---

## 2. Google APIs

---

## 2.1 Google Ads API

**Project path:** `Google\GoogleAds\`  
**Swagger UI:** `https://localhost:5001/swagger`  
**Google Base URL:** `https://googleads.googleapis.com/v18/customers/{customerId}/`  
**Auth:** OAuth 2.0 (scope: `https://www.googleapis.com/auth/adwords`) + `developer-token` header  

---

### Campaigns

| | |
|---|---|
| **Purpose** | Create and manage ad campaigns. Set budget, bidding strategy, advertising channel (Search/Display/Shopping/Video), start/end dates, and campaign status |
| **Our Endpoints** | `GET /api/googleads/campaigns` · `GET /api/googleads/campaigns/{customerId}/{resourceName}` · `POST /api/googleads/campaigns` · `PUT /api/googleads/campaigns` · `DELETE /api/googleads/campaigns/{customerId}/{resourceName}` |

**Request — Create Campaign:**
```
POST /api/googleads/campaigns
Authorization: Bearer {accessToken}

{
  "customerId": "1234567890",
  "name": "Brand Awareness Q1 2025",
  "advertisingChannelType": "SEARCH",
  "budgetAmountMicros": 5000000,
  "startDate": "20250201",
  "endDate": "20250331",
  "biddingStrategyType": "TARGET_CPA"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "resourceName": "customers/1234567890/campaigns/9876543210",
    "index": 0
  }
}
```

**Request — List Campaigns (with GAQL via Reports):**
```
GET /api/googleads/campaigns?customerId=1234567890&pageSize=50
Authorization: Bearer {accessToken}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "campaigns": [
      {
        "resourceName": "customers/1234567890/campaigns/9876543210",
        "id": 9876543210,
        "name": "Brand Awareness Q1 2025",
        "status": "ENABLED",
        "advertisingChannelType": "SEARCH",
        "biddingStrategyType": "TARGET_CPA",
        "budgetAmountMicros": 5000000,
        "startDate": "20250201",
        "endDate": "20250331",
        "servingStatus": "SERVING"
      }
    ],
    "nextPageToken": null
  }
}
```

---

### Ad Groups

| | |
|---|---|
| **Purpose** | Organize ads and keywords into themed groups within a campaign. Set default CPC bid and group type (Standard, Dynamic) |
| **Google Mutate URL** | `https://googleads.googleapis.com/v18/customers/{customerId}/adGroups:mutate` |
| **Our Endpoints** | `GET /api/googleads/adgroups` · `POST /api/googleads/adgroups` · `PUT /api/googleads/adgroups` · `DELETE /api/googleads/adgroups/{customerId}/{resourceName}` |

**Request — Create Ad Group:**
```
POST /api/googleads/adgroups
Authorization: Bearer {accessToken}

{
  "customerId": "1234567890",
  "campaignResourceName": "customers/1234567890/campaigns/9876543210",
  "name": "Running Shoes — Exact Match",
  "type": "SEARCH_STANDARD",
  "cpcBidMicros": 2000000
}
```

---

### Ads (Responsive Search Ads)

| | |
|---|---|
| **Purpose** | Create responsive search ads with multiple headline and description variants. Google automatically tests combinations to find top performers |
| **Google Mutate URL** | `https://googleads.googleapis.com/v18/customers/{customerId}/adGroupAds:mutate` |
| **Our Endpoints** | `GET /api/googleads/ads` · `POST /api/googleads/ads` · `PUT /api/googleads/ads` · `DELETE /api/googleads/ads/{customerId}/{resourceName}` |

**Request — Create Responsive Search Ad:**
```
POST /api/googleads/ads
Authorization: Bearer {accessToken}

{
  "customerId": "1234567890",
  "adGroupResourceName": "customers/1234567890/adGroups/111222333",
  "headlines": [
    "Best Running Shoes Online",
    "Free Shipping on All Orders",
    "Shop 500+ Shoe Styles Today"
  ],
  "descriptions": [
    "Award-winning comfort technology. Try 30 days risk-free.",
    "Find your perfect pair. Expert fit guarantee."
  ],
  "finalUrls": ["https://example.com/running-shoes"]
}
```

---

### Keywords

| | |
|---|---|
| **Purpose** | Add search keywords to ad groups. Control match type (Broad/Phrase/Exact) and individual CPC bids. Monitor quality score |
| **Google Mutate URL** | `https://googleads.googleapis.com/v18/customers/{customerId}/adGroupCriteria:mutate` |
| **Our Endpoints** | `GET /api/googleads/keywords` · `POST /api/googleads/keywords` · `PUT /api/googleads/keywords` · `DELETE /api/googleads/keywords/{customerId}/{resourceName}` |

**Request — Create Keyword:**
```
POST /api/googleads/keywords
Authorization: Bearer {accessToken}

{
  "customerId": "1234567890",
  "adGroupResourceName": "customers/1234567890/adGroups/111222333",
  "text": "running shoes",
  "matchType": "EXACT",
  "cpcBidMicros": 1500000
}
```

---

### Customers

| | |
|---|---|
| **Purpose** | Retrieve details of managed/sub-accounts and list all accounts accessible to the authenticated user |
| **Google Base URL** | `https://googleads.googleapis.com/v18/customers:listAccessibleCustomers` |
| **Our Endpoints** | `GET /api/googleads/customers/accessible` · `GET /api/googleads/customers/{customerId}` |

---

### Reports (GAQL)

| | |
|---|---|
| **Purpose** | Query any Google Ads data using Google Ads Query Language (GAQL) — similar to SQL. Retrieve performance metrics: impressions, clicks, CTR, CPC, conversions, ROAS, etc. |
| **Google Search URL** | `https://googleads.googleapis.com/v18/customers/{customerId}/googleAds:search` |
| **Our Endpoint** | `POST /api/googleads/reports/run` |

**Request:**
```
POST /api/googleads/reports/run
Authorization: Bearer {accessToken}

{
  "customerId": "1234567890",
  "query": "SELECT campaign.id, campaign.name, metrics.impressions, metrics.clicks, metrics.ctr, metrics.average_cpc, metrics.conversions, metrics.cost_micros FROM campaign WHERE segments.date DURING LAST_30_DAYS ORDER BY metrics.impressions DESC",
  "pageSize": 1000
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "rows": [
      {
        "fields": {
          "campaign.id": "9876543210",
          "campaign.name": "Brand Awareness Q1 2025",
          "metrics.impressions": "45230",
          "metrics.clicks": "1820",
          "metrics.ctr": "0.0402",
          "metrics.average_cpc": "850000",
          "metrics.conversions": "38.0",
          "metrics.cost_micros": "1547000000"
        }
      }
    ],
    "nextPageToken": null,
    "totalResultsCount": 12
  }
}
```

---

### Budgets, Audiences, Conversion Actions, Bidding Strategies

| API | Purpose | Our Endpoint |
|---|---|---|
| **Budgets** | Create shared/dedicated daily or monthly campaign budgets | `GET/POST /api/googleads/budgets` |
| **Audiences** | Create remarketing lists and in-market audience segments | `GET/POST /api/googleads/audiences` |
| **Conversions** | Track website purchases, form fills, phone calls, app installs | `GET/POST /api/googleads/conversions` |
| **Bidding Strategies** | Portfolio strategies: Target CPA, Target ROAS, Maximize Conversions, Maximize Conversion Value | `GET/POST /api/googleads/bidding-strategies` |

---

## 2.2 Google Workspace — Gmail

**Project path:** `Google\Workspace\`  
**Google Base URL:** `https://gmail.googleapis.com/gmail/v1/users/{userId}/`  
**Auth:** OAuth 2.0 — scopes: `https://www.googleapis.com/auth/gmail.modify` · `https://mail.google.com/`

---

### Messages

| | |
|---|---|
| **Purpose** | List, read, send, and manage emails. Apply/remove labels (read/unread/starred), trash, delete, and get raw MIME content |
| **Our Endpoints** | `GET /api/workspace/gmail/messages/{userId}` · `GET /api/workspace/gmail/messages/{userId}/{messageId}` · `POST /api/workspace/gmail/messages/{userId}/send` · `POST /api/workspace/gmail/messages/{userId}/modify` · `POST /api/workspace/gmail/messages/{userId}/trash` · `DELETE /api/workspace/gmail/messages/{userId}` |

**Request — Send Email:**
```
POST /api/workspace/gmail/messages/me/send
Authorization: Bearer {accessToken}

{
  "to": ["recipient@example.com", "cc-person@example.com"],
  "cc": ["manager@example.com"],
  "subject": "Meeting Tomorrow at 10am",
  "body": "Hi team, please join the Zoom call at 10am.",
  "htmlBody": "<p>Hi team, please join the <b>Zoom call</b> at 10am.</p>",
  "replyTo": "noreply@company.com"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": "18d7c2b4a9e10f3c",
    "threadId": "18d7c2b4a9e10f3c",
    "labelIds": ["SENT"],
    "snippet": "Hi team, please join the Zoom call at 10am."
  }
}
```

**Request — Modify Labels:**
```
POST /api/workspace/gmail/messages/me/modify

{
  "messageId": "18d7c2b4a9e10f3c",
  "addLabelIds": ["STARRED"],
  "removeLabelIds": ["UNREAD"]
}
```

---

### Labels

| | |
|---|---|
| **Purpose** | Create custom folders/tags in Gmail. Organize email with colored labels that appear in the sidebar |
| **Our Endpoints** | `GET/POST/PUT/DELETE /api/workspace/gmail/labels` |

**Request — Create Label:**
```
POST /api/workspace/gmail/labels/{userId}
Authorization: Bearer {accessToken}

{
  "name": "Invoices 2025",
  "messageListVisibility": "show",
  "labelListVisibility": "labelShow",
  "textColor": "#ffffff",
  "backgroundColor": "#1a73e8"
}
```

---

### Drafts & Threads

| API | Purpose | Our Endpoint |
|---|---|---|
| **Drafts** | Create, edit, send or delete email drafts | `GET/POST/PUT/DELETE /api/workspace/gmail/drafts` |
| **Threads** | Manage entire email conversations; apply bulk label changes | `GET/POST /api/workspace/gmail/threads` |
| **Settings** | Configure vacation auto-reply, forwarding, IMAP/POP, filters | `GET/PUT /api/workspace/gmail/settings` |

---

## 2.3 Google Workspace — Drive

**Google Base URL:** `https://www.googleapis.com/drive/v3/files`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/drive`

---

### Files

| | |
|---|---|
| **Purpose** | Create, read, update, delete files and folders. Upload content, generate shareable links, star, trash, and search by MIME type or owner |
| **Our Endpoints** | `GET /api/workspace/drive/files/{userId}` · `POST /api/workspace/drive/files/{userId}` · `POST /api/workspace/drive/files/{userId}/folder` · `PATCH /api/workspace/drive/files/{userId}` · `POST /api/workspace/drive/files/{userId}/share` · `DELETE /api/workspace/drive/files/{userId}` |

**Request — Create Folder:**
```
POST /api/workspace/drive/files/me/folder
Authorization: Bearer {accessToken}

{
  "name": "Project Alpha 2025",
  "parents": ["root"]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgVE2upms",
    "name": "Project Alpha 2025",
    "mimeType": "application/vnd.google-apps.folder",
    "webViewLink": "https://drive.google.com/drive/folders/1BxiMVs0...",
    "createdTime": "2025-01-09T10:00:00.000Z"
  }
}
```

**Request — Share File:**
```
POST /api/workspace/drive/files/me/share
Authorization: Bearer {accessToken}

{
  "fileId": "1BxiMVs0XRA5nFMdKvBd...",
  "type": "user",
  "role": "writer",
  "emailAddress": "colleague@company.com",
  "sendNotificationEmail": true
}
```

| Drive API | Purpose |
|---|---|
| **Permissions** | Grant/revoke access — roles: owner/organizer/writer/commenter/reader |
| **Revisions** | Version history — restore previous file versions |
| **Shared Drives** | Manage team-owned drives with membership and restrictions |

---

## 2.4 Google Workspace — Calendar

**Google Base URL:** `https://www.googleapis.com/calendar/v3/`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/calendar`

---

### Events

| | |
|---|---|
| **Purpose** | Create, update, and delete calendar events. Invite attendees, set recurring rules (RRULE), add Google Meet links, manage reminders |
| **Our Endpoints** | `GET /api/workspace/calendar/events/{userId}` · `POST /api/workspace/calendar/events/{userId}` · `PATCH /api/workspace/calendar/events/{userId}` · `POST /api/workspace/calendar/events/{userId}/quickAdd` · `DELETE /api/workspace/calendar/events/{userId}` |

**Request — Create Event:**
```
POST /api/workspace/calendar/events/me
Authorization: Bearer {accessToken}

{
  "calendarId": "primary",
  "summary": "Team Sprint Planning",
  "description": "Q1 Sprint kickoff for engineering team",
  "location": "Conference Room A",
  "start": { "dateTime": "2025-02-03T09:00:00", "timeZone": "America/New_York" },
  "end":   { "dateTime": "2025-02-03T10:30:00", "timeZone": "America/New_York" },
  "attendees": ["dev1@company.com", "dev2@company.com", "pm@company.com"],
  "recurrence": ["RRULE:FREQ=WEEKLY;BYDAY=MO"],
  "sendNotifications": true,
  "reminders": [
    { "method": "email",  "minutes": 1440 },
    { "method": "popup",  "minutes": 15   }
  ],
  "conferenceDataVersion": 1
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": "7v0iqmd0hn5os8k5mrdug5eo8i",
    "summary": "Team Sprint Planning",
    "start": { "dateTime": "2025-02-03T09:00:00-05:00" },
    "end":   { "dateTime": "2025-02-03T10:30:00-05:00" },
    "htmlLink": "https://www.google.com/calendar/event?eid=...",
    "status": "confirmed",
    "attendees": [
      { "email": "dev1@company.com", "responseStatus": "needsAction" }
    ]
  }
}
```

**Request — Check Free/Busy:**
```
POST /api/workspace/calendar/freebusy/me
Authorization: Bearer {accessToken}

{
  "timeMin": "2025-02-03T08:00:00Z",
  "timeMax": "2025-02-03T18:00:00Z",
  "timeZone": "America/New_York",
  "items": ["primary", "team-calendar@company.com"]
}
```

---

## 2.5 Google Workspace — Sheets

**Google Base URL:** `https://sheets.googleapis.com/v4/spreadsheets/`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/spreadsheets`

---

### Values & Spreadsheets

| | |
|---|---|
| **Purpose** | Create spreadsheets, read/write cell values, batch update ranges, append rows, clear ranges. Add/delete/rename sheets. Used for automated reporting, data pipelines, and dynamic dashboards |
| **Our Endpoints** | `POST /api/workspace/sheets/{userId}` · `GET /api/workspace/sheets/{userId}/{spreadsheetId}` · `GET /api/workspace/sheets/{userId}/{spreadsheetId}/values` · `PUT /api/workspace/sheets/{userId}/values` · `POST /api/workspace/sheets/{userId}/values/append` · `POST /api/workspace/sheets/{userId}/values/batchUpdate` |

**Request — Write Values to Sheet:**
```
PUT /api/workspace/sheets/me/values
Authorization: Bearer {accessToken}

{
  "spreadsheetId": "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs",
  "range": "Sheet1!A1:D3",
  "values": [
    ["Name",     "Email",                "Revenue", "Date"      ],
    ["Alice",    "alice@example.com",    "5000",    "2025-01-09"],
    ["Bob",      "bob@example.com",      "7200",    "2025-01-09"]
  ],
  "valueInputOption": "USER_ENTERED"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "spreadsheetId": "1BxiMVs0XRA5...",
    "updatedRange": "Sheet1!A1:D3",
    "updatedRows": 3,
    "updatedColumns": 4,
    "updatedCells": 12
  }
}
```

---

## 2.6 Google Workspace — Docs

**Google Base URL:** `https://docs.googleapis.com/v1/documents/`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/documents`

| Endpoint | Purpose |
|---|---|
| `POST /api/workspace/docs/{userId}` | Create new Google Doc with a title |
| `GET /api/workspace/docs/{userId}/{documentId}` | Get full document content (structural elements, paragraphs, tables) |
| `POST /api/workspace/docs/{userId}/insert-text` | Insert text at a specific character index |
| `POST /api/workspace/docs/{userId}/replace-text` | Find and replace text across the document |
| `POST /api/workspace/docs/{userId}/insert-table` | Insert a table at a location |
| `POST /api/workspace/docs/{userId}/paragraph-style` | Apply heading style (H1–H6, Normal) |

---

## 2.7 Google Workspace — Slides

**Google Base URL:** `https://slides.googleapis.com/v1/presentations/`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/presentations`

| Endpoint | Purpose |
|---|---|
| `POST /api/workspace/slides/{userId}` | Create new presentation |
| `GET /api/workspace/slides/{userId}/{presentationId}` | Get all slides, shapes, images, and text |
| `POST /api/workspace/slides/{userId}/add-slide` | Add slide with a predefined layout |
| `POST /api/workspace/slides/{userId}/insert-text` | Insert text into a shape |
| `POST /api/workspace/slides/{userId}/insert-image` | Insert image by URL with position and size |
| `POST /api/workspace/slides/{userId}/duplicate-slide` | Duplicate a slide |
| `POST /api/workspace/slides/{userId}/reorder-slides` | Change slide order |

---

## 2.8 Google Workspace — Meet

**Google Base URL:** `https://meet.googleapis.com/v2/`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/meetings.space.created`

| Endpoint | Purpose |
|---|---|
| `POST /api/workspace/meet/spaces/{userId}` | Create a meeting space with join URI and meeting code |
| `GET /api/workspace/meet/spaces/{userId}/{spaceName}` | Get meeting space details |
| `POST /api/workspace/meet/spaces/{userId}/{spaceName}/end` | End active meeting |
| `GET /api/workspace/meet/recordings/{userId}` | List recordings for a conference |
| `GET /api/workspace/meet/participants/{userId}` | List participants in a conference |

**Response — Create Meeting Space:**
```json
{
  "success": true,
  "data": {
    "name": "spaces/jQCFfuBOdKE",
    "meetingUri": "https://meet.google.com/abc-defg-hij",
    "meetingCode": "abc-defg-hij",
    "config": { "accessType": "OPEN" }
  }
}
```

---

## 2.9 Google Workspace — Chat

**Google Base URL:** `https://chat.googleapis.com/v1/`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/chat.messages`

| Endpoint | Purpose |
|---|---|
| `POST /api/workspace/chat/spaces/{userId}` | Create a Chat space or group |
| `GET /api/workspace/chat/messages/{userId}` | List messages in a space |
| `POST /api/workspace/chat/messages/{userId}` | Send a message with text or rich cards |
| `PUT /api/workspace/chat/messages/{userId}` | Edit a message |
| `DELETE /api/workspace/chat/messages/{userId}` | Delete a message |
| `GET /api/workspace/chat/members/{userId}` | List space members |
| `POST /api/workspace/chat/members/{userId}` | Add member to space |

---

## 2.10 Google Workspace — Forms

**Google Base URL:** `https://forms.googleapis.com/v1/forms/`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/forms.body`

| Endpoint | Purpose |
|---|---|
| `POST /api/workspace/forms/{userId}` | Create a new Google Form |
| `GET /api/workspace/forms/{userId}/{formId}` | Get form structure and items |
| `PUT /api/workspace/forms/{userId}` | Update form title and description |
| `POST /api/workspace/forms/{userId}/items` | Add question (short answer, MCQ, checkbox, dropdown, scale, date) |
| `GET /api/workspace/forms/responses/{userId}` | List form responses with answers |

---

## 2.11 Google Maps Platform

**Project path:** `Google\Maps\`  
**Swagger UI:** `https://localhost:5004/swagger`  
**Auth:** API Key (`?key={apiKey}` query parameter — no OAuth required)

---

### Geocoding

| | |
|---|---|
| **Purpose** | Convert human-readable addresses to coordinates (forward geocoding) and coordinates to addresses (reverse geocoding) |
| **Google Base URL** | `https://maps.googleapis.com/maps/api/geocode/json` |
| **Our Endpoints** | `GET /api/maps/geocode/address` · `GET /api/maps/geocode/reverse` |

**Request — Forward Geocode:**
```
GET /api/maps/geocode/address?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&language=en
```

**Response:**
```json
{
  "success": true,
  "data": {
    "status": "OK",
    "results": [
      {
        "placeId": "ChIJ2eUgeAK6j4ARbn5u_wAGqWA",
        "formattedAddress": "1600 Amphitheatre Pkwy, Mountain View, CA 94043, USA",
        "geometry": {
          "location": { "lat": 37.4224764, "lng": -122.0842499 },
          "locationType": "ROOFTOP"
        },
        "addressComponents": [
          { "longName": "1600",         "shortName": "1600",    "types": ["street_number"] },
          { "longName": "Amphitheatre Parkway", "shortName": "Amphitheatre Pkwy", "types": ["route"] },
          { "longName": "Mountain View","shortName": "Mountain View","types": ["locality"] },
          { "longName": "California",   "shortName": "CA",      "types": ["administrative_area_level_1"] },
          { "longName": "United States","shortName": "US",      "types": ["country"] }
        ]
      }
    ]
  }
}
```

---

### Directions

| | |
|---|---|
| **Purpose** | Calculate routes between origin and destination. Supports driving, walking, bicycling, and transit. Returns step-by-step turn instructions, distance, duration, and toll/highway avoidance |
| **Google Base URL** | `https://maps.googleapis.com/maps/api/directions/json` |
| **Our Endpoint** | `GET /api/maps/directions` |

**Request:**
```
GET /api/maps/directions?origin=New+York,NY&destination=Boston,MA&mode=driving&avoid=tolls&units=imperial
```

**Response:**
```json
{
  "success": true,
  "data": {
    "status": "OK",
    "routes": [
      {
        "summary": "I-95 N",
        "legs": [
          {
            "distance": { "value": 346276, "text": "215 mi" },
            "duration": { "value": 13500,  "text": "3 hours 45 mins" },
            "startAddress": "New York, NY, USA",
            "endAddress":   "Boston, MA, USA",
            "steps": [
              {
                "htmlInstructions": "Head <b>north</b> on 5th Ave",
                "distance": { "value": 1200, "text": "0.7 mi" },
                "duration": { "value": 240,  "text": "4 mins" },
                "travelMode": "DRIVING"
              }
            ]
          }
        ]
      }
    ]
  }
}
```

---

### Distance Matrix

| | |
|---|---|
| **Purpose** | Calculate travel distance and time between multiple origins and destinations simultaneously — ideal for delivery route planning and logistics |
| **Google Base URL** | `https://maps.googleapis.com/maps/api/distancematrix/json` |
| **Our Endpoint** | `GET /api/maps/distance-matrix` |

**Request:**
```
GET /api/maps/distance-matrix?origins=New+York,NY|Philadelphia,PA&destinations=Boston,MA|Washington,DC&mode=driving&units=metric
```

**Response:**
```json
{
  "success": true,
  "data": {
    "status": "OK",
    "originAddresses": ["New York, NY, USA", "Philadelphia, PA, USA"],
    "destinationAddresses": ["Boston, MA, USA", "Washington, DC, USA"],
    "rows": [
      {
        "elements": [
          { "status": "OK", "distance": { "value": 346276, "text": "346 km" }, "duration": { "value": 13500, "text": "3 hours 45 mins" } },
          { "status": "OK", "distance": { "value": 361400, "text": "361 km" }, "duration": { "value": 14700, "text": "4 hours 5 mins"  } }
        ]
      }
    ]
  }
}
```

---

### Places

| | |
|---|---|
| **Purpose** | Discover nearby businesses, search by text query, get detailed place info (hours, photos, reviews, ratings), and power address autocomplete in search boxes |
| **Google Base URL** | `https://maps.googleapis.com/maps/api/place/` |
| **Our Endpoints** | `GET /api/maps/places/nearby` · `GET /api/maps/places/search` · `GET /api/maps/places/details` · `GET /api/maps/places/autocomplete` |

**Request — Nearby Search:**
```
GET /api/maps/places/nearby?lat=37.7749&lng=-122.4194&radius=1000&type=restaurant&openNow=true&maxPrice=2
```

**Request — Autocomplete:**
```
GET /api/maps/places/autocomplete?input=Googleplex&language=en&types=establishment
```

**Response — Autocomplete:**
```json
{
  "success": true,
  "data": {
    "status": "OK",
    "predictions": [
      {
        "description": "Googleplex, Amphitheatre Pkwy, Mountain View, CA, USA",
        "placeId": "ChIJ2eUgeAK6j4ARbn5u_wAGqWA",
        "types": ["establishment", "point_of_interest"],
        "structuredFormatting": {
          "mainText": "Googleplex",
          "secondaryText": "Amphitheatre Pkwy, Mountain View, CA, USA"
        }
      }
    ]
  }
}
```

---

### Other Maps APIs

| API | Purpose | Our Endpoint |
|---|---|---|
| **Elevation** | Get terrain elevation for GPS coordinates or along a path | `GET /api/maps/elevation/locations` · `/path` |
| **Time Zone** | Get UTC offset and DST offset for any coordinate at a timestamp | `GET /api/maps/timezone` |
| **Roads — Snap to Roads** | Snap raw GPS coordinates to the nearest road segment | `GET /api/maps/roads/snap` |
| **Static Maps** | Generate a static map image URL with markers, paths, and styles | `GET /api/maps/static` |

---

## 2.12 Google Analytics (GA4)

**Project path:** `Google\Analytics\`  
**Swagger UI:** `https://localhost:5005/swagger`  
**Google Base URL:** `https://analyticsdata.googleapis.com/v1beta/properties/{propertyId}/`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/analytics.readonly`

---

### Reports

| | |
|---|---|
| **Purpose** | Query GA4 data with custom dimensions and metrics. Analyze user behavior, session data, conversion funnels, traffic sources, and e-commerce performance |
| **Our Endpoints** | `POST /api/analytics/reports/run` · `POST /api/analytics/reports/batch` · `POST /api/analytics/reports/pivot` · `POST /api/analytics/reports/funnel` |

**Request — Standard Report:**
```
POST /api/analytics/reports/run
Authorization: Bearer {accessToken}

{
  "propertyId": "properties/123456789",
  "dateRanges": [
    { "startDate": "2024-01-01", "endDate": "2024-12-31", "name": "Year 2024" }
  ],
  "dimensions": [
    { "name": "date" },
    { "name": "sessionSource" },
    { "name": "deviceCategory" }
  ],
  "metrics": [
    { "name": "sessions" },
    { "name": "totalUsers" },
    { "name": "bounceRate" },
    { "name": "conversions" },
    { "name": "revenue" }
  ],
  "orderBys": [{ "metric": { "metricName": "sessions" }, "desc": true }],
  "limit": 10000
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "dimensionHeaders": [
      { "name": "date" }, { "name": "sessionSource" }, { "name": "deviceCategory" }
    ],
    "metricHeaders": [
      { "name": "sessions",   "type": "TYPE_INTEGER" },
      { "name": "totalUsers", "type": "TYPE_INTEGER" },
      { "name": "bounceRate", "type": "TYPE_FLOAT"   },
      { "name": "conversions","type": "TYPE_FLOAT"   },
      { "name": "revenue",    "type": "TYPE_FLOAT"   }
    ],
    "rows": [
      {
        "dimensionValues": [{ "value": "20240115" }, { "value": "google" }, { "value": "desktop" }],
        "metricValues":    [{ "value": "4521" }, { "value": "3820" }, { "value": "0.42" }, { "value": "182" }, { "value": "9450.50" }]
      }
    ],
    "rowCount": 1250
  }
}
```

---

### Realtime, Metadata, Properties

| API | Purpose | Our Endpoint |
|---|---|---|
| **Realtime Report** | Active users, events, and page views in the last 30 minutes | `POST /api/analytics/realtime/report` |
| **Metadata** | All available dimensions and metrics for a property | `GET /api/analytics/metadata/{propertyId}` |
| **Properties** | List GA4 accounts, properties, and data streams | `GET /api/analytics/properties` |

---

## 2.13 Google Cloud Vision AI

**Project path:** `Google\Cloud\`  
**Swagger UI:** `https://localhost:5006/swagger`  
**Google Base URL:** `https://vision.googleapis.com/v1/images:annotate`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/cloud-platform`

---

### Image Annotation

| | |
|---|---|
| **Purpose** | Detect objects, faces, text (OCR), landmarks, logos, and explicit content in images. Also analyzes image properties (dominant colors) and performs web entity detection |

**Request — Multi-Feature Annotation:**
```
POST /api/cloud/vision/annotate
Authorization: Bearer {accessToken}

{
  "projectId": "my-project-id",
  "image": {
    "imageUri": "https://example.com/photo.jpg"
  },
  "features": [
    { "type": "LABEL_DETECTION",      "maxResults": 10 },
    { "type": "TEXT_DETECTION",       "maxResults": 50 },
    { "type": "FACE_DETECTION",       "maxResults": 10 },
    { "type": "LANDMARK_DETECTION",   "maxResults": 5  },
    { "type": "LOGO_DETECTION",       "maxResults": 5  },
    { "type": "SAFE_SEARCH_DETECTION","maxResults": 1  },
    { "type": "WEB_DETECTION",        "maxResults": 10 }
  ]
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "labelAnnotations": [
      { "mid": "/m/0bt9lr", "description": "Sky",     "score": 0.9921, "topicality": 0.9921 },
      { "mid": "/m/01g5v",  "description": "Mountain", "score": 0.9780, "topicality": 0.9780 }
    ],
    "textAnnotations": [
      { "description": "Welcome\nto\nColorado",  "boundingPoly": { ... } }
    ],
    "faceAnnotations": [
      {
        "detectionConfidence": 0.9952,
        "joyLikelihood": "VERY_LIKELY",
        "sorrowLikelihood": "VERY_UNLIKELY",
        "angerLikelihood": "VERY_UNLIKELY",
        "rollAngle": 2.5, "panAngle": -15.0, "tiltAngle": 3.2
      }
    ],
    "safeSearchAnnotation": {
      "adult": "VERY_UNLIKELY",
      "spoof": "UNLIKELY",
      "medical": "VERY_UNLIKELY",
      "violence": "VERY_UNLIKELY",
      "racy": "UNLIKELY"
    },
    "webDetection": {
      "bestGuessLabels": [{ "label": "Colorado mountain road" }],
      "webEntities": [
        { "entityId": "/m/0bk0h", "score": 0.621, "description": "Colorado" }
      ]
    }
  }
}
```

---

## 2.14 Google Cloud Speech-to-Text

**Google Base URL:** `https://speech.googleapis.com/v1/speech:recognize`

| | |
|---|---|
| **Purpose** | Convert audio (MP3, FLAC, WAV, OGG, WebM) to text with speaker diarization, profanity filtering, automatic punctuation, and word-level timestamps |

**Request — Recognize (sync, up to 1 min):**
```
POST /api/cloud/speech/recognize
Authorization: Bearer {accessToken}

{
  "projectId": "my-project-id",
  "config": {
    "encoding": "FLAC",
    "sampleRateHertz": 16000,
    "languageCode": "en-US",
    "maxAlternatives": 1,
    "enableAutomaticPunctuation": true,
    "enableSpeakerDiarization": true,
    "diarizationSpeakerCount": 2,
    "model": "video",
    "useEnhanced": true
  },
  "audio": {
    "uri": "gs://my-bucket/audio/meeting-recording.flac"
  }
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "results": [
      {
        "alternatives": [
          {
            "transcript": "Hello everyone, welcome to today's meeting. Let's get started with the agenda.",
            "confidence": 0.9842,
            "words": [
              { "startTime": "0s",    "endTime": "0.400s", "word": "Hello",    "confidence": 0.99, "speakerTag": 1 },
              { "startTime": "0.400s","endTime": "0.800s", "word": "everyone", "confidence": 0.98, "speakerTag": 1 }
            ]
          }
        ],
        "resultEndTime": "4.200s",
        "languageCode": "en-us"
      }
    ],
    "totalBilledTime": "5s"
  }
}
```

> For audio longer than 1 minute, use `POST /api/cloud/speech/long-running` which returns an operation name; poll `GET /api/cloud/speech/operations/{name}` for results.

---

## 2.15 Google Cloud Natural Language

**Google Base URL:** `https://language.googleapis.com/v1/documents:`

| Endpoint | Purpose | Use Case |
|---|---|---|
| `POST /api/cloud/language/entities` | Extract named entities (people, places, dates, phone numbers, prices) | CRM data extraction, document indexing |
| `POST /api/cloud/language/sentiment` | Measure overall and per-sentence sentiment score (-1.0 negative → +1.0 positive) | Product reviews, support tickets, social media |
| `POST /api/cloud/language/syntax` | Tokenize and parse grammar (parts of speech, dependency tree) | Content analysis, translation preprocessing |
| `POST /api/cloud/language/classify` | Categorize text into 700+ predefined content categories | News classification, ad targeting |
| `POST /api/cloud/language/moderate` | Detect toxic content, hate speech, insults, and threats with confidence scores | Content moderation, community platforms |
| `POST /api/cloud/language/annotate` | Run all analyses in a single request | Full document understanding |

**Request — Analyze Sentiment:**
```
POST /api/cloud/language/sentiment
Authorization: Bearer {accessToken}

{
  "projectId": "my-project-id",
  "document": {
    "type": "PLAIN_TEXT",
    "content": "I absolutely love this product! The quality is amazing and delivery was super fast.",
    "language": "en"
  },
  "encodingType": "UTF8"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "documentSentiment": { "magnitude": 2.8, "score": 0.9 },
    "language": "en",
    "sentences": [
      { "text": { "content": "I absolutely love this product!", "beginOffset": 0 },
        "sentiment": { "magnitude": 1.8, "score": 0.9 } },
      { "text": { "content": "The quality is amazing and delivery was super fast.", "beginOffset": 33 },
        "sentiment": { "magnitude": 1.6, "score": 0.9 } }
    ]
  }
}
```

---

## 2.16 Google Cloud Translation

**Google Base URL:** `https://translation.googleapis.com/v3/projects/{projectId}:`

| Endpoint | Purpose |
|---|---|
| `POST /api/cloud/translation/translate` | Translate text into any of 130+ languages |
| `POST /api/cloud/translation/detect` | Auto-detect the language of input text |
| `GET /api/cloud/translation/languages` | List all 130+ supported language codes |
| `POST /api/cloud/translation/romanize` | Transliterate non-Latin scripts (Hindi, Arabic, Chinese) to Latin alphabet |
| `POST /api/cloud/translation/batch-translate` | Translate large documents stored in GCS asynchronously |
| `POST /api/cloud/translation/glossaries` | Create custom glossaries for domain-specific terminology |

**Request — Translate:**
```
POST /api/cloud/translation/translate
Authorization: Bearer {accessToken}

{
  "projectId": "my-project-id",
  "contents": [
    "Hello, how are you?",
    "Good morning, have a great day!"
  ],
  "targetLanguageCode": "ja",
  "sourceLanguageCode": "en",
  "mimeType": "text/plain"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "translations": [
      { "translatedText": "こんにちは、お元気ですか？",      "detectedLanguageCode": "en" },
      { "translatedText": "おはようございます、良い一日を！", "detectedLanguageCode": "en" }
    ]
  }
}
```

---

## 2.17 Google BigQuery

**Project path:** `Google\BigQuery\`  
**Swagger UI:** `https://localhost:5007/swagger`  
**Google Base URL:** `https://bigquery.googleapis.com/bigquery/v2/projects/{projectId}/`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/bigquery`

---

### Jobs & Queries

| | |
|---|---|
| **Purpose** | Run SQL queries against petabytes of data, stream rows into tables, export results to GCS, schedule batch loads from CSV/JSON/Parquet files |

**Request — Run Query:**
```
POST /api/bigquery/jobs/query
Authorization: Bearer {accessToken}

{
  "projectId": "my-project-id",
  "query": "SELECT country, SUM(revenue) AS total_revenue, COUNT(*) AS order_count FROM `my-project.sales.orders` WHERE DATE(created_at) >= '2024-01-01' GROUP BY country ORDER BY total_revenue DESC LIMIT 20",
  "maxResults": 1000,
  "useLegacySql": false,
  "location": "US",
  "timeoutMs": 30000
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "jobComplete": true,
    "schema": {
      "fields": [
        { "name": "country",       "type": "STRING",  "mode": "NULLABLE" },
        { "name": "total_revenue", "type": "FLOAT64", "mode": "NULLABLE" },
        { "name": "order_count",   "type": "INTEGER", "mode": "NULLABLE" }
      ]
    },
    "rows": [
      { "f": [{ "v": "United States" }, { "v": "4520089.50" }, { "v": "18420" }] },
      { "f": [{ "v": "United Kingdom" }, { "v": "1820450.00" }, { "v": "7320"  }] }
    ],
    "totalRows": 45,
    "totalBytesProcessed": "52428800"
  }
}
```

**Request — Stream Insert (InsertAll):**
```
POST /api/bigquery/jobs/insert-all
Authorization: Bearer {accessToken}

{
  "projectId": "my-project",
  "datasetId": "sales",
  "tableId": "events",
  "rows": [
    { "insertId": "event_001", "json": { "user_id": "u123", "event": "purchase", "amount": 99.99, "timestamp": "2025-01-09T10:30:00Z" } },
    { "insertId": "event_002", "json": { "user_id": "u456", "event": "signup",   "amount": 0,     "timestamp": "2025-01-09T10:31:00Z" } }
  ],
  "skipInvalidRows": false,
  "ignoreUnknownValues": false
}
```

| BigQuery API | Purpose | Our Endpoint |
|---|---|---|
| **Datasets** | Create/manage logical containers for tables | `GET/POST/PUT/DELETE /api/bigquery/datasets` |
| **Tables** | Define schema, create partitioned/clustered tables, copy tables | `GET/POST/PUT/DELETE /api/bigquery/tables` |
| **Jobs** | Async query, load, export operations with status polling | `GET/POST /api/bigquery/jobs` |
| **Routines** | Create SQL UDFs and stored procedures | `GET/POST/DELETE /api/bigquery/routines` |

---

## 2.18 Google Custom Search

**Project path:** `Google\Search\`  
**Swagger UI:** `https://localhost:5008/swagger`  
**Google Base URL:** `https://www.googleapis.com/customsearch/v1`  
**Auth:** API Key only (no OAuth required)

| | |
|---|---|
| **Purpose** | Programmatic web and image search using a custom search engine. Filter by date, file type, site, language, reading level. Retrieve 10 results per request (max 100 per day free tier) |

**Request — Web Search:**
```
GET /api/search/custom?q=climate+change+solutions+2025&num=10&start=1&language=en&dateRestrict=m1&safe=medium
```

**Response:**
```json
{
  "success": true,
  "data": {
    "searchInformation": {
      "totalResults": "1840000",
      "searchTime": 0.42,
      "formattedTotalResults": "1,840,000"
    },
    "items": [
      {
        "title":       "Top Climate Solutions for 2025 | Nature.com",
        "link":        "https://www.nature.com/articles/climate-solutions-2025",
        "displayLink": "www.nature.com",
        "snippet":     "Scientists outline five key interventions that could limit warming to 1.5°C by 2050...",
        "pagemap": {}
      }
    ],
    "queries": {
      "nextPage": [{ "startIndex": 11, "count": 10 }]
    }
  }
}
```

---

## 2.19 Google Knowledge Graph

**Google Base URL:** `https://kgsearch.googleapis.com/v1/entities:search`  
**Auth:** API Key

| | |
|---|---|
| **Purpose** | Search Google's Knowledge Graph for structured entity data: people, places, organizations, events. Returns Wikidata-linked data with descriptions and images |

**Request:**
```
GET /api/search/knowledge-graph?query=Elon+Musk&types=Person&languageCode=en&limit=5
```

**Response:**
```json
{
  "success": true,
  "data": {
    "itemListElement": [
      {
        "result": {
          "id": "kg:/m/06f0p",
          "name": "Elon Musk",
          "type": { "id": "Person" },
          "description": "Business magnate, investor, and entrepreneur",
          "detailedDescription": {
            "articleBody": "Elon Reeve Musk is a business magnate and investor. He is the founder...",
            "url": "https://en.wikipedia.org/wiki/Elon_Musk"
          },
          "image": { "contentUrl": "https://upload.wikimedia.org/wikipedia/commons/..." }
        },
        "resultScore": 4108.15
      }
    ]
  }
}
```

---

## 2.20 Google Identity & People API

**Project path:** `Google\Identity\`  
**Swagger UI:** `https://localhost:5009/swagger`

### OAuth 2.0

| Our Endpoint | Purpose |
|---|---|
| `GET /api/identity/oauth/url` | Generate Google Sign-In URL with requested scopes |
| `GET /api/identity/oauth/callback` | Exchange authorization code for access + ID tokens |
| `POST /api/identity/oauth/refresh` | Silently refresh expired access token |
| `POST /api/identity/oauth/revoke` | Revoke user consent and invalidate tokens |
| `GET /api/identity/oauth/token-info` | Inspect an access token (expiry, scopes, email) |
| `GET /api/identity/oauth/userinfo` | Get authenticated user's profile (name, email, photo) |

### People API (Contacts)

**Google Base URL:** `https://people.googleapis.com/v1/`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/contacts`

| Our Endpoint | Purpose |
|---|---|
| `GET /api/identity/people/me` | Get authenticated user's own profile |
| `GET /api/identity/people/connections` | List all Google Contacts |
| `GET /api/identity/people/search?query=John` | Search contacts by name or email |
| `POST /api/identity/people` | Create a new contact |
| `PATCH /api/identity/people` | Update contact fields |
| `DELETE /api/identity/people/{resourceName}` | Delete a contact |
| `GET /api/identity/people/groups` | List contact groups (labels) |
| `POST /api/identity/people/groups` | Create a contact group |

### Identity Toolkit (Firebase Auth REST)

**Google Base URL:** `https://identitytoolkit.googleapis.com/v1/`  
**Auth:** API Key

| Our Endpoint | Purpose |
|---|---|
| `POST /api/identity/toolkit/signup` | Create new user with email + password |
| `POST /api/identity/toolkit/signin` | Sign in and get ID token |
| `POST /api/identity/toolkit/account` | Get user profile from ID token |
| `PUT /api/identity/toolkit/profile` | Update display name, photo URL |
| `PUT /api/identity/toolkit/password` | Change password |
| `PUT /api/identity/toolkit/email` | Change email address |
| `POST /api/identity/toolkit/verify-email` | Send email verification link |
| `POST /api/identity/toolkit/reset-password` | Send password reset email |
| `DELETE /api/identity/toolkit/account` | Delete user account |

---

## 2.21 Firebase Cloud Messaging (FCM)

**Project path:** `Google\Firebase\`  
**Swagger UI:** `https://localhost:5010/swagger`  
**Google Base URL:** `https://fcm.googleapis.com/v1/projects/{projectId}/messages:send`  
**Auth:** OAuth 2.0 — scope: `https://www.googleapis.com/auth/firebase.messaging`

| | |
|---|---|
| **Purpose** | Send push notifications to iOS, Android, and Web browsers. Target individual devices by token, broadcast to topics, or segment by conditions |

**Request — Send to Device Token:**
```
POST /api/firebase/fcm/send/token
Authorization: Bearer {accessToken}

{
  "projectId": "my-firebase-project",
  "token": "fxxx_token_here...",
  "title": "Your order has shipped!",
  "body": "Your package #12345 is on its way. ETA: tomorrow by 8pm.",
  "imageUrl": "https://cdn.example.com/shipping-icon.png",
  "data": {
    "orderId": "12345",
    "trackingUrl": "https://example.com/track/12345",
    "deepLink": "app://orders/12345"
  },
  "android": {
    "priority": "high",
    "notification": {
      "channelId": "orders",
      "notificationPriority": "PRIORITY_HIGH",
      "sound": "default"
    }
  }
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "name": "projects/my-firebase-project/messages/1234567890"
  }
}
```

**Request — Send to Topic:**
```
POST /api/firebase/fcm/send/topic
Authorization: Bearer {accessToken}

{
  "projectId": "my-firebase-project",
  "topic": "breaking-news",
  "title": "Breaking: Major earthquake in Pacific",
  "body": "A 7.2 magnitude earthquake struck off the coast...",
  "data": { "category": "news", "priority": "high" }
}
```

**Request — Multicast (up to 500 tokens):**
```
POST /api/firebase/fcm/send/multicast
Authorization: Bearer {accessToken}

{
  "projectId": "my-firebase-project",
  "tokens": ["token_1...", "token_2...", "token_3..."],
  "title": "Flash Sale! 50% off next 2 hours",
  "body": "Don't miss out — limited stock available!",
  "data": { "saleId": "flash_001", "discount": "50" }
}
```

---

## 2.22 Firebase Auth & Remote Config

### Firebase Auth

**Google Base URL:** `https://identitytoolkit.googleapis.com/v1/`

| Our Endpoint | Purpose |
|---|---|
| `POST /api/firebase/users/signup` | Register new user |
| `POST /api/firebase/users/signin` | Sign in, get ID token |
| `GET /api/firebase/users/account` | Get user info from ID token |
| `PUT /api/firebase/users/profile` | Update display name / photo |
| `POST /api/firebase/users/verify-token` | Verify ID token server-side |
| `POST /api/firebase/users/custom-token` | Mint custom auth token for UID with claims |
| `GET /api/firebase/users/admin/list` | Admin: list all users |
| `POST /api/firebase/users/admin` | Admin: create user record |
| `PUT /api/firebase/users/admin` | Admin: update user (disable, verify email) |
| `DELETE /api/firebase/users/admin/{localId}` | Admin: delete user |

### Firebase Remote Config

**Google Base URL:** `https://firebaseremoteconfig.googleapis.com/v1/projects/{projectId}/remoteConfig`

| Our Endpoint | Purpose |
|---|---|
| `GET /api/firebase/remote-config/{projectId}` | Fetch current remote config template with all parameter values |
| `PUT /api/firebase/remote-config/{projectId}` | Publish updated remote config (requires ETag for version control) |
| `GET /api/firebase/remote-config/{projectId}/versions` | List config version history |
| `POST /api/firebase/remote-config/{projectId}/rollback` | Roll back to a previous version |

### Firebase App Check

| Our Endpoint | Purpose |
|---|---|
| `POST /api/firebase/app-check/verify` | Verify a Firebase App Check token (prevents abuse) |
| `GET /api/firebase/app-check/configs` | Get App Check enforcement config per app |
| `PATCH /api/firebase/app-check/configs` | Set enforcement mode: UNENFORCED or ENFORCED |

---

## 3. Flipkart Seller APIs

**Project path:** `Flipkart\`  
**Swagger UI:** Start Flipkart Web project → `/swagger`  
**Google Base URL:** `https://api.flipkart.net/sellers/`  
**Auth:** OAuth 2.0 — seller access token obtained from Flipkart Seller Hub  

> Flipkart's Open API is available to approved marketplace sellers in India only.

---

### 3.1 Listings API

| | |
|---|---|
| **Purpose** | Create, update, and manage product listings (SKUs) on Flipkart marketplace. Control pricing, inventory, tax info, fulfillment mode, and listing status |
| **Our Endpoints** | `POST /api/flipkart/listings` · `PUT /api/flipkart/listings` · `GET /api/flipkart/listings/{skuIds}` · `POST /api/flipkart/listings/update/price` · `POST /api/flipkart/listings/update/inventory` |

**Request — Create Listing:**
```
POST /api/flipkart/listings
Authorization: Bearer {flipkartAccessToken}

{
  "skuId": "SELLER-SKU-001",
  "fsn": "SHOEXYZ12345",
  "listingStatus": "ACTIVE",
  "mrp": 1999,
  "sellingPrice": 1499,
  "stock": 50,
  "listingCurrency": "INR",
  "weight": 500,
  "shippingProvider": "FLIPKART",
  "taxCode": "GST_18"
}
```

**Request — Update Price:**
```
POST /api/flipkart/listings/update/price
Authorization: Bearer {flipkartAccessToken}

{
  "skuId": "SELLER-SKU-001",
  "mrp": 1999,
  "sellingPrice": 1299,
  "listingCurrency": "INR"
}
```

**Request — Update Inventory:**
```
POST /api/flipkart/listings/update/inventory
Authorization: Bearer {flipkartAccessToken}

{
  "skuId": "SELLER-SKU-001",
  "stock": 100,
  "fulfillmentCenter": "FC_DELHI_01"
}
```

---

### 3.2 Orders (Shipments) API

| | |
|---|---|
| **Purpose** | Complete order fulfillment lifecycle: receive orders, generate labels and invoices, mark dispatch, track shipments, manage cancellations, and handle self-ship |
| **Our Endpoints** | 20+ endpoints under `api/flipkart/orders/` |

| Endpoint | Purpose | Key Request Fields |
|---|---|---|
| `POST /api/flipkart/orders/shipments/filter` | Filter orders by state, SKU, date | `states`, `skuIds`, `locationId`, `startDate`, `endDate` |
| `POST /api/flipkart/orders/shipments/labels` | Generate packing labels + invoice PDF | `shipmentIds`, `includeTax`, `includeInvoice` |
| `GET /api/flipkart/orders/shipments/{ids}/labels` | Download label PDF | shipmentId path param |
| `GET /api/flipkart/orders/shipments/{ids}/invoices` | Download invoice PDF | shipmentId path param |
| `POST /api/flipkart/orders/shipments/dispatch` | Mark as dispatched (triggers tracking) | `shipmentIds`, `dispatchBy` |
| `POST /api/flipkart/orders/shipments/cancel` | Cancel shipments | `shipmentIds`, `reason` |
| `POST /api/flipkart/orders/shipments/manifest` | Generate pickup manifest for courier | `locationId`, `shipmentIds` |
| `POST /api/flipkart/orders/shipments/self-ship/dispatch` | Dispatch via own logistics | `shipmentId`, `trackingId`, `provider` |
| `POST /api/flipkart/orders/shipments/self-ship/delivery` | Mark delivered for self-ship | `shipmentId`, `deliveredAt` |
| `POST /api/flipkart/orders/services/attempt` | Record failed delivery attempt | `shipmentId`, `attemptTime`, `reason` |
| `POST /api/flipkart/orders/services/complete` | Mark service/delivery complete | `shipmentId`, `completedAt` |

**Request — Filter Shipments:**
```
POST /api/flipkart/orders/shipments/filter
Authorization: Bearer {flipkartAccessToken}

{
  "states": ["APPROVED", "PACKING_IN_PROGRESS"],
  "type": "SELLER_FULFILLED",
  "locationId": "LOC_001",
  "startDate": "2025-01-01T00:00:00Z",
  "endDate": "2025-01-09T23:59:59Z",
  "pageSize": 50
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "shipments": [
      {
        "shipmentId": "SHP123456789",
        "orderId": "OD123456789",
        "orderDate": "2025-01-08T14:22:00Z",
        "state": "APPROVED",
        "items": [
          {
            "skuId": "SELLER-SKU-001",
            "quantity": 2,
            "sellingPrice": 1499,
            "title": "Running Shoes - Size 10"
          }
        ],
        "shippingAddress": {
          "name": "John Doe",
          "city": "Mumbai",
          "pincode": "400001",
          "state": "Maharashtra"
        },
        "pickupSlot": "2025-01-10T10:00:00Z"
      }
    ],
    "totalCount": 145,
    "hasMore": true,
    "nextCursor": "abc123..."
  }
}
```

---

### 3.3 Returns API

| | |
|---|---|
| **Purpose** | Manage product returns end-to-end: view return requests, approve/reject returns, schedule pickup, record condition, and complete return processing |

| Endpoint | Purpose |
|---|---|
| `GET /api/flipkart/returns` | List return requests with filters (state, date, SKU) |
| `POST /api/flipkart/returns/approve` | Approve return and initiate pickup |
| `POST /api/flipkart/returns/reject` | Reject return with reason |
| `POST /api/flipkart/returns/pickup` | Schedule courier pickup |
| `POST /api/flipkart/returns/pickup-attempt` | Record a pickup attempt |
| `POST /api/flipkart/returns/complete` | Complete return after quality check |
| `POST /api/flipkart/returns/tasks/initial-load` | Receive initial return task batch (OTC) |
| `POST /api/flipkart/returns/otc/start` | Start OTC (Over-The-Counter) return session |
| `POST /api/flipkart/returns/tasks/shipments` | Get scanned shipments in OTC session |
| `POST /api/flipkart/returns/otc/end` | Complete OTC return session |

---

### 3.4 Reports API

| | |
|---|---|
| **Purpose** | Request and download seller performance reports: sales, inventory, returns, payments, and listing health |

| Endpoint | Purpose |
|---|---|
| `POST /api/flipkart/reports` | Request a report (type: SALES/INVENTORY/RETURNS/PAYMENT) |
| `GET /api/flipkart/reports/{reportId}` | Check status and get download URL |

**Request:**
```
POST /api/flipkart/reports
Authorization: Bearer {flipkartAccessToken}

{
  "reportType": "SALES",
  "startDate": "2025-01-01",
  "endDate": "2025-01-31",
  "format": "CSV"
}
```

---

## 4. Authentication Reference

### Google OAuth 2.0 Flow (All Google + Firebase APIs)

```
1. GET  /api/{service}/auth/url
       → Returns: { "authUrl": "https://accounts.google.com/o/oauth2/v2/auth?..." }

2. User visits authUrl → logs in → Google redirects to:
       GET /api/{service}/auth/callback?code=4/xxxx&state=...

3. Backend exchanges code:
       POST https://oauth2.googleapis.com/token
       Body: code, client_id, client_secret, redirect_uri, grant_type=authorization_code
       Returns: { access_token, refresh_token, expires_in, token_type }

4. Use access_token in header: Authorization: Bearer {access_token}

5. When expired (3600s), refresh:
       POST /api/{service}/auth/token/refresh
       Body: { "refreshToken": "1//0g...", "userId/customerId/projectId": "..." }
```

### Google Maps / Custom Search / Knowledge Graph (API Key)

```
No OAuth required. All requests append ?key={YOUR_API_KEY}
Managed in: appsettings.json → Google:Maps:ApiKey / Google:Search:ApiKey
```

### Google Ads (Additional Header)

```
All requests must include:
Authorization: Bearer {access_token}
developer-token: {YOUR_DEVELOPER_TOKEN}
login-customer-id: {MANAGER_ACCOUNT_ID}    (optional, for MCC)
```

### Flipkart Seller OAuth

```
Token endpoint: https://api.flipkart.net/oauth-service/oauth/token
Grant type: client_credentials (app-level) or authorization_code (seller-level)
All requests: Authorization: Bearer {flipkartAccessToken}
```

### Standard Response Envelope (All Implemented APIs)

```json
{
  "success": true,
  "data": { ... },
  "message": null
}

{
  "success": false,
  "data": null,
  "message": "Access token not found.",
  "errorCode": 401
}
```

---

## 5. Facebook / Meta APIs (Reference)

> **Status:** Not yet implemented in this project. Documented here as reference for future implementation.

### 5.1 Facebook Graph API

| | |
|---|---|
| **Base URL** | `https://graph.facebook.com/v20.0/` |
| **Auth** | OAuth 2.0 — User Access Token or Page Access Token |
| **Developer Portal** | developers.facebook.com |

**Key APIs:**

| API | Base URL | Purpose |
|---|---|---|
| **Pages API** | `/v20.0/{page-id}` | Manage Facebook Pages: posts, photos, videos, events, reviews |
| **Instagram Graph API** | `https://graph.instagram.com/v20.0/` | Instagram Business/Creator accounts: media, stories, insights, comments |
| **Facebook Ads** | `/v20.0/act_{ad-account-id}/` | Create and manage ad campaigns, ad sets, ads; query insights |
| **WhatsApp Business** | `/v20.0/{phone-number-id}/messages` | Send WhatsApp messages (text, template, interactive, media) |
| **Messenger Platform** | `/v20.0/me/messages` | Send/receive Messenger messages, manage subscription webhooks |
| **Webhooks** | `/v20.0/{app-id}/subscriptions` | Subscribe to real-time events (page posts, comments, lead ads) |
| **Facebook Login** | `https://www.facebook.com/v20.0/dialog/oauth` | User authentication via Facebook |
| **Catalog API** | `/v20.0/{catalog-id}/products` | Manage product catalog for Dynamic Ads and Commerce |

### 5.2 Facebook Pages API

**Request — Create Page Post:**
```
POST https://graph.facebook.com/v20.0/{page-id}/feed
Authorization: Bearer {pageAccessToken}

{
  "message": "Excited to announce our new product launch! 🚀",
  "link": "https://example.com/new-product",
  "published": true
}
```

**Response:**
```json
{ "id": "{page-id}_{post-id}" }
```

**Request — Get Page Insights:**
```
GET https://graph.facebook.com/v20.0/{page-id}/insights
  ?metric=page_impressions,page_reach,page_engaged_users,page_fan_adds
  &period=day
  &since=2025-01-01
  &until=2025-01-31
  &access_token={pageAccessToken}
```

### 5.3 Instagram Graph API

| Endpoint | Purpose |
|---|---|
| `GET /v20.0/{ig-user-id}/media` | List all Instagram posts with metrics |
| `POST /v20.0/{ig-user-id}/media` | Create media container for image/video post |
| `POST /v20.0/{ig-user-id}/media_publish` | Publish the media container |
| `GET /v20.0/{ig-media-id}/comments` | Get comments on a post |
| `POST /v20.0/{ig-media-id}/replies` | Reply to a comment |
| `GET /v20.0/{ig-user-id}/insights` | Account-level analytics (reach, impressions, follower count) |
| `GET /v20.0/{ig-media-id}/insights` | Post-level analytics (likes, saves, shares, reach) |
| `GET /v20.0/{ig-user-id}/mentions` | Get @mentions of the account |
| `GET /v20.0/{ig-hashtag-id}/recent_media` | Search posts by hashtag |

**Request — Publish Image Post:**
```
Step 1 — Create container:
POST https://graph.instagram.com/v20.0/{ig-user-id}/media
  ?image_url=https://example.com/photo.jpg
  &caption=Check out our latest collection! #fashion #style
  &access_token={accessToken}

Response: { "id": "17889456972...container_id" }

Step 2 — Publish:
POST https://graph.instagram.com/v20.0/{ig-user-id}/media_publish
  ?creation_id=17889456972...
  &access_token={accessToken}

Response: { "id": "17889456972...media_id" }
```

### 5.4 Facebook Ads API

**Base URL:** `https://graph.facebook.com/v20.0/`  
**Auth:** User Access Token with `ads_management` permission

| Object | Endpoint | Purpose |
|---|---|---|
| **Ad Account** | `/act_{account-id}` | Account details, spending limits, currency |
| **Campaigns** | `/act_{account-id}/campaigns` | Create campaigns with objective (CONVERSIONS/REACH/TRAFFIC/BRAND_AWARENESS) |
| **Ad Sets** | `/act_{account-id}/adsets` | Define audience targeting, budget, schedule, placements |
| **Ads** | `/act_{account-id}/ads` | Create individual ads with creative |
| **Ad Creatives** | `/act_{account-id}/adcreatives` | Image, video, carousel, collection creatives |
| **Insights** | `/{campaign-id}/insights` | Performance metrics: impressions, reach, clicks, CTR, CPC, ROAS, spend |

**Request — Create Campaign:**
```
POST https://graph.facebook.com/v20.0/act_123456789/campaigns
Authorization: Bearer {accessToken}

{
  "name": "Summer Sale 2025",
  "objective": "OUTCOME_SALES",
  "status": "PAUSED",
  "special_ad_categories": [],
  "budget_rebalance_flag": false
}
```

**Request — Get Insights:**
```
GET https://graph.facebook.com/v20.0/{ad-account-id}/insights
  ?fields=campaign_name,impressions,reach,clicks,ctr,cpc,spend,actions,roas
  &date_preset=last_30d
  &level=campaign
  &access_token={accessToken}
```

**Response:**
```json
{
  "data": [
    {
      "campaign_name": "Summer Sale 2025",
      "impressions": "45230",
      "reach": "38420",
      "clicks": "1820",
      "ctr": "4.023",
      "cpc": "0.82",
      "spend": "1492.40",
      "actions": [
        { "action_type": "purchase", "value": "182" },
        { "action_type": "add_to_cart", "value": "540" }
      ],
      "date_start": "2024-12-10",
      "date_stop": "2025-01-09"
    }
  ],
  "paging": { "cursors": { "before": "...", "after": "..." } }
}
```

### 5.5 WhatsApp Business API

**Base URL:** `https://graph.facebook.com/v20.0/{phone-number-id}/messages`  
**Auth:** System User Access Token

| Message Type | Purpose |
|---|---|
| **Template Message** | Pre-approved transactional messages (OTP, order status, shipping updates) |
| **Text Message** | Free-form text (only within 24-hour conversation window) |
| **Interactive** | Buttons, list menus, reply buttons for guided flows |
| **Media** | Image, audio, video, document, sticker messages |
| **Location** | Share coordinates with name and address |
| **Contacts** | Send vCard contact information |
| **Reaction** | React to a message with an emoji |

**Request — Send Template (OTP):**
```
POST https://graph.facebook.com/v20.0/{phone-number-id}/messages
Authorization: Bearer {systemUserToken}

{
  "messaging_product": "whatsapp",
  "to": "919876543210",
  "type": "template",
  "template": {
    "name": "otp_verification",
    "language": { "code": "en_US" },
    "components": [
      {
        "type": "body",
        "parameters": [
          { "type": "text", "text": "847392" }
        ]
      }
    ]
  }
}
```

**Response:**
```json
{
  "messaging_product": "whatsapp",
  "contacts": [{ "input": "919876543210", "wa_id": "919876543210" }],
  "messages": [{ "id": "wamid.HBgLMTY1MDUwNTU1NRUCABIYFjNFQjBDMUM0RUZCM0Q5OTdBQzk0QzIA" }]
}
```

### 5.6 Facebook Webhooks

**Setup URL:** `POST https://graph.facebook.com/v20.0/{app-id}/subscriptions`

**Subscribable Events:**

| Object | Fields | Purpose |
|---|---|---|
| `page` | `feed, messages, mention` | Page post activity, DMs, mentions |
| `instagram` | `comments, mentions, story_insights` | IG comment activity |
| `whatsapp_business_account` | `messages, message_status` | WA message delivery and read receipts |
| `leadgen` | `leadgen` | New Facebook Lead Ads submissions in real-time |
| `user` | `email, friends` | User data changes (requires user consent) |

---

## Summary Statistics

| Platform | Total APIs | Implemented | Status |
|---|---|---|---|
| YouTube | 12 modules | 12 | ✅ Built & compiling |
| Google Ads | 11 modules | 11 | ✅ Built & compiling |
| Google Workspace | 25 modules | 25 | ✅ Built & compiling |
| Google Maps | 8 modules | 8 | ✅ Built & compiling |
| Google Analytics GA4 | 5 modules | 5 | ✅ Built & compiling |
| Google Cloud AI | 5 modules | 5 | ✅ Built & compiling |
| Google BigQuery | 5 modules | 5 | ✅ Built & compiling |
| Google Search | 2 modules | 2 | ✅ Built & compiling |
| Google Identity | 4 modules | 4 | ✅ Built & compiling |
| Firebase | 5 modules | 5 | ✅ Built & compiling |
| Flipkart | 5 modules | 5 | ✅ Built & compiling |
| **Facebook/Meta** | **8+ modules** | **0** | 📋 Reference only |
| **TOTAL** | **95 modules** | **87** | |

> **51 Playwright + xUnit tests** at `Google\Tests\` — run `dotnet test` after starting APIs.

---

## 6. Google Marketing APIs (added 2026-10-30)

Six new modules plus additions to Google Ads and Google Analytics. Every endpoint was checked against Google's official reference and discovery documents. Each endpoint, its upstream Google URL and its verification status are listed in `API_LIST.csv`.

| Module | Project path | Port | Endpoints | Google API (version) |
|---|---|---|---|---|
| Search Console + Indexing | `Google\SearchConsole\` | 5011 | 17 | Search Console API (webmasters v3, searchconsole v1), Indexing API v3 |
| Business Profile | `Google\BusinessProfile\` | 5012 | 81 | Account Management, Business Information, Verifications, Place Actions, Notifications, Lodging, Performance (v1) + reviews/posts/media (v4) |
| Tag Manager | `Google\TagManager\` | 5013 | 108 | Tag Manager API v2 (all resources) |
| Campaign Manager 360 | `Google\CampaignManager\` | 5014 | 221 | Campaign Manager 360 API v5 (dfareporting) |
| Display & Video 360 | `Google\DisplayVideo\` | 5015 | 180 | Display & Video 360 API v4 |
| Merchant Center | `Google\Merchant\` | 5016 | 143 | Merchant API v1 (all 13 sub-APIs) + reviews/Product Studio v1alpha |
| Google Ads (added) | `Google\GoogleAds\` → `api/googleads/rest/...` | 5001 | +177 | Google Ads API **v26** (every REST method: Keyword Planner, Reach/Audience planning, Shopping, Display, Demand Gen/YouTube, Performance Max, conversions upload, batch jobs, experiments, recommendations) |
| Analytics (added) | `Google\Analytics\` | 5005 | +19 | GA4 Data API v1beta (batch pivot, compatibility, audience exports) + v1alpha (audience lists, report tasks, quotas, chat) |

### Setup (new modules)
1. Point `ConnectionStrings:DefaultConnection` at your SQL Server (on a named instance, put it in `appsettings.Development.json`, which is git-ignored). **The database creates itself:** on startup each module runs its `Database\*.sql` scripts (idempotent), creating the database, tables and stored procedures. Google Ads (`Database\GoogleAds.sql`) and Analytics (`Database\Analytics.sql`) work the same way, so the `.bak` restore is no longer needed. Set `"Database": { "AutoInitialize": false }` to turn it off.
2. In Google Cloud Console, enable the matching API and create an OAuth 2.0 Web client. Add the redirect URI `https://localhost:<port>/api/<module>/auth/callback`.
3. Put `ClientId` / `ClientSecret` into `ApiToolKit.Web\appsettings.json` under `Google:<Module>`.
4. `dotnet run --project Google\<Module>\ApiToolKit.Web`, then open `https://localhost:<port>/swagger`.
5. Call `GET /api/<module>/auth/url?accountKey=<any-name>`, sign in, and Google returns to the callback, which stores the tokens under that `accountKey`.
6. Pass the same `accountKey` to every endpoint. Tokens refresh automatically.

Google Ads `rest` endpoints use the stored token of `customerId`, which comes from the existing `/api/googleads/auth` flow. Set `Google:Ads:LoginCustomerId` when you work through a manager (MCC) account.

### Fixes in existing modules
- Google Ads moved from **v18 (sunset, every call failed)** to **v26**. The version is set in one place: `GoogleAdsApiVersion.cs`. New campaigns now send `containsEuPoliticalAdvertising`, which Google requires.
- Google Ads and Analytics token responses now map Google's snake_case fields (`access_token`, ...). Before this fix, stored tokens were always empty.
- GA4 funnel reports now call `v1alpha` (`runFunnelReport` does not exist in v1beta).

### Not included (and why)
- **Google Trends API**: alpha with application-only access and no public reference.
- **Business Profile Q&A API**: discontinued by Google on 3 Nov 2025. **v4 account/location methods** were replaced by the v1 APIs above.
- **Search Console Mobile-Friendly Test API**: retired by Google in Dec 2023.
- **5 file-upload methods** (CM360 `creativeAssets.insert`, `studioCreativeAssets.insert`; DV360 `assets.upload`, `adAssets.upload`, `media.upload`): these need multipart uploads and are not generated yet.
- Google Ads **Video** campaigns can only be read, not created, through the API (Google restriction). Use Demand Gen for YouTube ads.
