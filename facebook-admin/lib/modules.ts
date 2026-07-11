import type { Module } from './types';

const B = 'http://localhost:5150'; // base — replaced at runtime by AppContext

export const MODULES: Module[] = [
  // ── 01. AUTH ─────────────────────────────────────────────────────────────
  {
    id: 'auth', num: '01', name: 'Auth', group: 'Core',
    endpoints: [
      { id: 'auth-url', label: 'Generate Auth URL', method: 'POST', url: `${B}/api/facebook/auth/auth-url`, body: '{"scope":["public_profile","email","business_management"],"state":"login1"}' },
      { id: 'app-token', label: 'Get App Token', method: 'GET', url: `${B}/api/facebook/auth/app-token` },
      { id: 'token-debug', label: 'Debug Token', method: 'POST', url: `${B}/api/facebook/auth/token/debug`, body: '{"inputToken":"{TOKEN}"}' },
      { id: 'token-refresh', label: 'Refresh Token', method: 'POST', url: `${B}/api/facebook/auth/token/refresh`, body: '{"accessToken":"{TOKEN}"}' },
      { id: 'token-long-lived', label: 'Get Long-Lived Token', method: 'POST', url: `${B}/api/facebook/auth/token/long-lived`, body: '{"shortLivedToken":"{TOKEN}"}' },
    ],
  },

  // ── 02. USER ──────────────────────────────────────────────────────────────
  {
    id: 'user', num: '02', name: 'User', group: 'Core',
    endpoints: [
      { id: 'user-profile', label: 'Get Profile', method: 'GET', url: `${B}/api/facebook/user/{USER_ID}/profile?accessToken={TOKEN}` },
      { id: 'user-friends', label: 'Get Friends', method: 'GET', url: `${B}/api/facebook/user/{USER_ID}/friends?accessToken={TOKEN}` },
      { id: 'user-permissions', label: 'Get Permissions', method: 'GET', url: `${B}/api/facebook/user/{USER_ID}/permissions?accessToken={TOKEN}` },
      { id: 'user-picture', label: 'Get Picture', method: 'GET', url: `${B}/api/facebook/user/{USER_ID}/picture?accessToken={TOKEN}` },
      { id: 'user-feed', label: 'Get Feed', method: 'GET', url: `${B}/api/facebook/user/{USER_ID}/feed?accessToken={TOKEN}` },
      { id: 'user-pages', label: 'Get Pages', method: 'GET', url: `${B}/api/facebook/user/{USER_ID}/pages?accessToken={TOKEN}` },
      { id: 'user-ad-accounts', label: 'Get Ad Accounts', method: 'GET', url: `${B}/api/facebook/user/{USER_ID}/ad-accounts?accessToken={TOKEN}` },
    ],
  },

  // ── 03. BUSINESS MANAGER ─────────────────────────────────────────────────
  {
    id: 'business', num: '03', name: 'Business Manager', group: 'Core',
    endpoints: [
      { id: 'biz-get', label: 'Get Business', method: 'GET', url: `${B}/api/facebook/business/{BIZ_ID}?accessToken={TOKEN}` },
      { id: 'biz-pages', label: 'Get Pages', method: 'GET', url: `${B}/api/facebook/business/{BIZ_ID}/pages?accessToken={TOKEN}` },
      { id: 'biz-ad-accounts', label: 'Get Ad Accounts', method: 'GET', url: `${B}/api/facebook/business/{BIZ_ID}/ad-accounts?accessToken={TOKEN}` },
      { id: 'biz-ig', label: 'Get Instagram Accounts', method: 'GET', url: `${B}/api/facebook/business/{BIZ_ID}/instagram-accounts?accessToken={TOKEN}` },
      { id: 'biz-catalogs', label: 'Get Catalogs', method: 'GET', url: `${B}/api/facebook/business/{BIZ_ID}/catalogs?accessToken={TOKEN}` },
      { id: 'biz-users', label: 'Get Users', method: 'GET', url: `${B}/api/facebook/business/{BIZ_ID}/users?accessToken={TOKEN}` },
      { id: 'biz-system-users', label: 'Get System Users', method: 'GET', url: `${B}/api/facebook/business/{BIZ_ID}/system-users?accessToken={TOKEN}` },
      { id: 'biz-client-pages', label: 'Get Client Pages', method: 'GET', url: `${B}/api/facebook/business/{BIZ_ID}/client-pages?accessToken={TOKEN}` },
      { id: 'biz-user-add', label: 'Add User', method: 'POST', url: `${B}/api/facebook/business/{BIZ_ID}/users?accessToken={TOKEN}`, body: '{"userId":"{USER_ID}","role":"EMPLOYEE","email":"user@example.com"}' },
      { id: 'biz-user-del', label: 'Remove User', method: 'DELETE', url: `${B}/api/facebook/business/{BIZ_ID}/users/{USER_ID}?accessToken={TOKEN}` },
      { id: 'biz-claim-page', label: 'Claim Page', method: 'POST', url: `${B}/api/facebook/business/{BIZ_ID}/claim-page?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}"}' },
    ],
  },

  // ── 04. PAGE ─────────────────────────────────────────────────────────────
  {
    id: 'page', num: '04', name: 'Page', group: 'Content',
    endpoints: [
      { id: 'page-categories', label: 'Get Categories', method: 'GET', url: `${B}/api/facebook/page/categories?accessToken={TOKEN}` },
      { id: 'page-user-pages', label: 'Get User Pages', method: 'GET', url: `${B}/api/facebook/page/user/{USER_ID}/pages?accessToken={TOKEN}` },
      { id: 'page-info', label: 'Get Page Info', method: 'GET', url: `${B}/api/facebook/page/{PAGE_ID}/info?accessToken={TOKEN}` },
      { id: 'page-access-token', label: 'Get Page Access Token', method: 'GET', url: `${B}/api/facebook/page/{PAGE_ID}/access-token?userAccessToken={TOKEN}&accessToken={TOKEN}` },
      { id: 'page-settings', label: 'Update Settings', method: 'PUT', url: `${B}/api/facebook/page/{PAGE_ID}/settings?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","setting":"MESSAGING","value":"true"}' },
      { id: 'page-post', label: 'Create Post', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/post?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","message":"Test post from ApiToolKit"}' },
      { id: 'page-feed', label: 'Get Feed', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/feed?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","limit":10}' },
      { id: 'page-photos', label: 'Get Photos', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/photos?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","limit":10}' },
      { id: 'page-videos', label: 'Get Videos', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/videos?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","limit":10}' },
      { id: 'page-events', label: 'Get Events', method: 'GET', url: `${B}/api/facebook/page/{PAGE_ID}/events?accessToken={TOKEN}` },
      { id: 'page-picture', label: 'Update Picture', method: 'PUT', url: `${B}/api/facebook/page/{PAGE_ID}/picture?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","picture":"https://example.com/img.jpg"}' },
      { id: 'page-roles', label: 'Get Roles', method: 'GET', url: `${B}/api/facebook/page/{PAGE_ID}/roles?accessToken={TOKEN}` },
      { id: 'page-tabs-list', label: 'Get Tabs', method: 'GET', url: `${B}/api/facebook/page/{PAGE_ID}/tabs?accessToken={TOKEN}` },
      { id: 'page-tab-add', label: 'Add Tab', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/tabs?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","appId":"987654321"}' },
      { id: 'page-tab-update', label: 'Update Tab', method: 'PUT', url: `${B}/api/facebook/page/{PAGE_ID}/tabs/photos?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","tabId":"photos","customName":"Our Photos"}' },
      { id: 'page-tab-delete', label: 'Delete Tab', method: 'DELETE', url: `${B}/api/facebook/page/{PAGE_ID}/tabs/photos?accessToken={TOKEN}` },
      { id: 'page-milestones', label: 'Get Milestones', method: 'GET', url: `${B}/api/facebook/page/{PAGE_ID}/milestones?accessToken={TOKEN}` },
      { id: 'page-milestone-create', label: 'Create Milestone', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/milestones?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","title":"Founded","startTime":"2020-01-01"}' },
      { id: 'page-milestone-delete', label: 'Delete Milestone', method: 'DELETE', url: `${B}/api/facebook/page/{PAGE_ID}/milestones/milestone1?accessToken={TOKEN}` },
      { id: 'page-blocked-list', label: 'Get Blocked Users', method: 'GET', url: `${B}/api/facebook/page/{PAGE_ID}/blocked?accessToken={TOKEN}&limit=10` },
      { id: 'page-block-user', label: 'Block User', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/blocked?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","userId":"987654321"}' },
      { id: 'page-unblock-user', label: 'Unblock User', method: 'DELETE', url: `${B}/api/facebook/page/{PAGE_ID}/blocked/987654321?accessToken={TOKEN}` },
      { id: 'page-cta', label: 'Get CTA', method: 'GET', url: `${B}/api/facebook/page/{PAGE_ID}/cta?accessToken={TOKEN}` },
      { id: 'page-cta-create', label: 'Create CTA', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/cta?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","type":"LEARN_MORE","webUrl":"https://example.com"}' },
      { id: 'page-cta-delete', label: 'Delete CTA', method: 'DELETE', url: `${B}/api/facebook/page/{PAGE_ID}/cta/cta1?accessToken={TOKEN}` },
      { id: 'page-visitor-posts', label: 'Get Visitor Posts', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/visitor-posts?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","limit":10}' },
      { id: 'page-labels', label: 'Get Labels', method: 'GET', url: `${B}/api/facebook/page/{PAGE_ID}/labels?accessToken={TOKEN}` },
      { id: 'page-label-create', label: 'Create Label', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/labels?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","name":"VIP Customer"}' },
      { id: 'page-label-delete', label: 'Delete Label', method: 'DELETE', url: `${B}/api/facebook/page/{PAGE_ID}/labels/label1?accessToken={TOKEN}` },
      { id: 'page-label-assign', label: 'Assign Label', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/labels/assign?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","labelId":"label1","userId":"987654321"}' },
      { id: 'page-published-posts', label: 'Get Published Posts', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/published-posts?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","limit":10}' },
      { id: 'page-scheduled-posts', label: 'Get Scheduled Posts', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/scheduled-posts?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","limit":10}' },
      { id: 'page-promotable-posts', label: 'Get Promotable Posts', method: 'POST', url: `${B}/api/facebook/page/{PAGE_ID}/promotable-posts?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","limit":10}' },
    ],
  },

  // ── 05. POST ─────────────────────────────────────────────────────────────
  {
    id: 'post', num: '05', name: 'Post', group: 'Content',
    endpoints: [
      { id: 'post-get', label: 'Get Post', method: 'GET', url: `${B}/api/facebook/post/123456789?accessToken={TOKEN}` },
      { id: 'post-update', label: 'Update Post', method: 'PUT', url: `${B}/api/facebook/post/123456789?accessToken={TOKEN}`, body: '{"postId":"123456789","message":"updated message"}' },
      { id: 'post-delete', label: 'Delete Post', method: 'DELETE', url: `${B}/api/facebook/post/123456789?accessToken={TOKEN}` },
      { id: 'post-feed', label: 'Create Post on Feed', method: 'POST', url: `${B}/api/facebook/post/feed?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","message":"Hello from ApiToolKit","link":""}' },
      { id: 'post-comments', label: 'List Comments', method: 'POST', url: `${B}/api/facebook/post/123456789/comments?accessToken={TOKEN}`, body: '{"objectId":"123456789","limit":10}' },
      { id: 'post-reactions', label: 'List Reactions', method: 'POST', url: `${B}/api/facebook/post/123456789/reactions?accessToken={TOKEN}`, body: '{"objectId":"123456789","type":"LIKE"}' },
      { id: 'post-publish', label: 'Publish Post', method: 'POST', url: `${B}/api/facebook/post/123456789/publish?accessToken={TOKEN}`, body: '{"postId":"123456789"}' },
    ],
  },

  // ── 06. COMMENT ──────────────────────────────────────────────────────────
  {
    id: 'comment', num: '06', name: 'Comment', group: 'Content',
    endpoints: [
      { id: 'cmt-get', label: 'Get Comment', method: 'GET', url: `${B}/api/facebook/comment/123456789?accessToken={TOKEN}` },
      { id: 'cmt-list', label: 'List Comments', method: 'POST', url: `${B}/api/facebook/comment/list?accessToken={TOKEN}`, body: '{"objectId":"123456789","limit":10}' },
      { id: 'cmt-update', label: 'Update Comment', method: 'PUT', url: `${B}/api/facebook/comment/123456789?accessToken={TOKEN}`, body: '{"commentId":"123456789","message":"updated comment"}' },
      { id: 'cmt-delete', label: 'Delete Comment', method: 'DELETE', url: `${B}/api/facebook/comment/123456789?accessToken={TOKEN}` },
      { id: 'cmt-hide', label: 'Hide/Unhide Comment', method: 'POST', url: `${B}/api/facebook/comment/123456789/hide?accessToken={TOKEN}`, body: '{"commentId":"123456789","isHidden":true}' },
      { id: 'cmt-replies', label: 'Get Replies', method: 'GET', url: `${B}/api/facebook/comment/123456789/replies?accessToken={TOKEN}` },
      { id: 'cmt-like', label: 'Like Comment', method: 'POST', url: `${B}/api/facebook/comment/123456789/like?accessToken={TOKEN}` },
    ],
  },

  // ── 07. PHOTO ────────────────────────────────────────────────────────────
  {
    id: 'photo', num: '07', name: 'Photo', group: 'Content',
    endpoints: [
      { id: 'photo-upload', label: 'Upload Photo', method: 'POST', url: `${B}/api/facebook/photo/upload?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","url":"https://example.com/img.jpg","caption":"test"}' },
      { id: 'photo-get', label: 'Get Photo', method: 'GET', url: `${B}/api/facebook/photo/123456789?accessToken={TOKEN}` },
      { id: 'photo-list', label: 'List Photos', method: 'POST', url: `${B}/api/facebook/photo/list?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","limit":10}' },
      { id: 'photo-delete', label: 'Delete Photo', method: 'DELETE', url: `${B}/api/facebook/photo/123456789?accessToken={TOKEN}` },
      { id: 'photo-albums', label: 'Get Albums', method: 'GET', url: `${B}/api/facebook/photo/{PAGE_ID}/albums?accessToken={TOKEN}` },
      { id: 'photo-album-create', label: 'Create Album', method: 'POST', url: `${B}/api/facebook/photo/album?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","name":"Test Album","message":"Album description"}' },
      { id: 'photo-album-photos', label: 'Get Album Photos', method: 'GET', url: `${B}/api/facebook/photo/123456789/photos?accessToken={TOKEN}` },
    ],
  },

  // ── 08. VIDEO ────────────────────────────────────────────────────────────
  {
    id: 'video', num: '08', name: 'Video', group: 'Content',
    endpoints: [
      { id: 'video-upload', label: 'Upload Video', method: 'POST', url: `${B}/api/facebook/video/upload?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","fileUrl":"https://example.com/vid.mp4","title":"Test Video","description":"desc"}' },
      { id: 'video-get', label: 'Get Video', method: 'GET', url: `${B}/api/facebook/video/123456789?accessToken={TOKEN}` },
      { id: 'video-list', label: 'List Videos', method: 'POST', url: `${B}/api/facebook/video/list?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","limit":10}' },
      { id: 'video-update', label: 'Update Video', method: 'PUT', url: `${B}/api/facebook/video/123456789?accessToken={TOKEN}`, body: '{"title":"Updated Title","description":"updated desc"}' },
      { id: 'video-delete', label: 'Delete Video', method: 'DELETE', url: `${B}/api/facebook/video/123456789?accessToken={TOKEN}` },
      { id: 'video-status', label: 'Get Status', method: 'GET', url: `${B}/api/facebook/video/123456789/status?accessToken={TOKEN}` },
      { id: 'video-thumbnails', label: 'Get Thumbnails', method: 'GET', url: `${B}/api/facebook/video/123456789/thumbnails?accessToken={TOKEN}` },
    ],
  },

  // ── 09. EVENT ────────────────────────────────────────────────────────────
  {
    id: 'event', num: '09', name: 'Event', group: 'Content',
    endpoints: [
      { id: 'event-get', label: 'Get Event', method: 'GET', url: `${B}/api/facebook/event/123456789?accessToken={TOKEN}` },
      { id: 'event-update', label: 'Update Event', method: 'PUT', url: `${B}/api/facebook/event/123456789?accessToken={TOKEN}`, body: '{"eventId":"123456789","name":"Updated Event","startTime":"2026-08-01T10:00:00"}' },
      { id: 'event-delete', label: 'Delete Event', method: 'DELETE', url: `${B}/api/facebook/event/123456789?accessToken={TOKEN}` },
      { id: 'event-page-events', label: 'Get Page Events', method: 'GET', url: `${B}/api/facebook/event/{PAGE_ID}/events?accessToken={TOKEN}` },
      { id: 'event-attendees', label: 'Get Attendees', method: 'POST', url: `${B}/api/facebook/event/123456789/attendees?accessToken={TOKEN}`, body: '{"eventId":"123456789","limit":25}' },
      { id: 'event-rsvp', label: 'RSVP to Event', method: 'POST', url: `${B}/api/facebook/event/123456789/rsvp?accessToken={TOKEN}`, body: '{"eventId":"123456789","rsvp":"attending"}' },
    ],
  },

  // ── 10. GROUP ────────────────────────────────────────────────────────────
  {
    id: 'group', num: '10', name: 'Group', group: 'Content',
    endpoints: [
      { id: 'group-get', label: 'Get Group', method: 'GET', url: `${B}/api/facebook/group/123456789?accessToken={TOKEN}` },
      { id: 'group-user-groups', label: 'Get User Groups', method: 'GET', url: `${B}/api/facebook/group/user/{USER_ID}/groups?accessToken={TOKEN}` },
      { id: 'group-feed', label: 'Get Group Feed', method: 'POST', url: `${B}/api/facebook/group/123456789/feed?accessToken={TOKEN}`, body: '{"groupId":"123456789","limit":25}' },
      { id: 'group-members', label: 'Get Members', method: 'POST', url: `${B}/api/facebook/group/123456789/members?accessToken={TOKEN}`, body: '{"groupId":"123456789","limit":25}' },
      { id: 'group-post', label: 'Create Post in Group', method: 'POST', url: `${B}/api/facebook/group/123456789/post?accessToken={TOKEN}`, body: '{"groupId":"123456789","message":"Hello group!"}' },
      { id: 'group-albums', label: 'Get Albums', method: 'POST', url: `${B}/api/facebook/group/123456789/albums?accessToken={TOKEN}`, body: '{"groupId":"123456789"}' },
    ],
  },

  // ── 11. REACTION ─────────────────────────────────────────────────────────
  {
    id: 'reaction', num: '11', name: 'Reaction', group: 'Content',
    endpoints: [
      { id: 'reaction-list', label: 'List Reactions', method: 'POST', url: `${B}/api/facebook/reaction/list?accessToken={TOKEN}`, body: '{"objectId":"123456789","limit":10}' },
      { id: 'reaction-delete', label: 'Delete Reaction', method: 'DELETE', url: `${B}/api/facebook/reaction/123456789?accessToken={TOKEN}` },
    ],
  },

  // ── 12. INSIGHT ──────────────────────────────────────────────────────────
  {
    id: 'insight', num: '12', name: 'Insight', group: 'Analytics',
    endpoints: [
      { id: 'insight-page', label: 'Page Insights', method: 'POST', url: `${B}/api/facebook/insight/page?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","metric":["page_fans","page_views_total"],"period":"day"}' },
      { id: 'insight-video', label: 'Video Insights', method: 'POST', url: `${B}/api/facebook/insight/video?accessToken={TOKEN}`, body: '{"videoId":"123456789","metric":"total_video_views","period":"day"}' },
      { id: 'insight-ad', label: 'Ad Insights', method: 'POST', url: `${B}/api/facebook/insight/ad?accessToken={TOKEN}`, body: '{"adId":"123456789","accountId":"act_123456789","fields":["impressions","clicks","spend"]}' },
      { id: 'insight-campaign', label: 'Campaign Insights', method: 'GET', url: `${B}/api/facebook/insight/campaign/123456789?accessToken={TOKEN}` },
      { id: 'insight-adset', label: 'Ad Set Insights', method: 'GET', url: `${B}/api/facebook/insight/adset/123456789?accessToken={TOKEN}` },
      { id: 'insight-account', label: 'Account Insights', method: 'GET', url: `${B}/api/facebook/insight/account/act_123456789?accessToken={TOKEN}` },
    ],
  },

  // ── 13. AD ACCOUNT ───────────────────────────────────────────────────────
  {
    id: 'ad-account', num: '13', name: 'Ad Account', group: 'Advertising',
    endpoints: [
      { id: 'adacct-get', label: 'Get Ad Account', method: 'GET', url: `${B}/api/facebook/ad-account/act_123456789?accessToken={TOKEN}` },
      { id: 'adacct-user', label: 'Get User Ad Accounts', method: 'POST', url: `${B}/api/facebook/ad-account/user/ad-accounts?accessToken={TOKEN}`, body: '{"userId":"{USER_ID}"}' },
      { id: 'adacct-billing', label: 'Get Billing', method: 'GET', url: `${B}/api/facebook/ad-account/act_123456789/billing?accessToken={TOKEN}` },
      { id: 'adacct-add-user', label: 'Add User to Account', method: 'POST', url: `${B}/api/facebook/ad-account/act_123456789/users?accessToken={TOKEN}`, body: '{"accountId":"act_123456789","userId":"{USER_ID}","role":"ADMIN"}' },
      { id: 'adacct-campaigns', label: 'Get Campaigns', method: 'POST', url: `${B}/api/facebook/ad-account/act_123456789/campaigns?accessToken={TOKEN}`, body: '{"accountId":"act_123456789","limit":10}' },
      { id: 'adacct-creatives', label: 'Get Creatives', method: 'GET', url: `${B}/api/facebook/ad-account/act_123456789/creatives?accessToken={TOKEN}` },
    ],
  },

  // ── 14. CAMPAIGN ─────────────────────────────────────────────────────────
  {
    id: 'campaign', num: '14', name: 'Campaign', group: 'Advertising',
    endpoints: [
      { id: 'camp-create', label: 'Create Campaign', method: 'POST', url: `${B}/api/facebook/campaign?accessToken={TOKEN}`, body: '{"accountId":"act_123456789","name":"Test Campaign","objective":"OUTCOME_AWARENESS","status":"PAUSED","dailyBudget":1000}' },
      { id: 'camp-get', label: 'Get Campaign', method: 'GET', url: `${B}/api/facebook/campaign/123456789?accessToken={TOKEN}` },
      { id: 'camp-list', label: 'List Campaigns', method: 'POST', url: `${B}/api/facebook/campaign/list?accessToken={TOKEN}`, body: '{"accountId":"act_123456789","limit":10}' },
      { id: 'camp-update', label: 'Update Campaign', method: 'PUT', url: `${B}/api/facebook/campaign/123456789?accessToken={TOKEN}`, body: '{"campaignId":"123456789","name":"Updated Campaign","status":"ACTIVE"}' },
      { id: 'camp-delete', label: 'Delete Campaign', method: 'DELETE', url: `${B}/api/facebook/campaign/123456789?accessToken={TOKEN}` },
      { id: 'camp-adsets', label: 'Get Ad Sets', method: 'GET', url: `${B}/api/facebook/campaign/123456789/adsets?accessToken={TOKEN}` },
      { id: 'camp-ads', label: 'Get Ads', method: 'GET', url: `${B}/api/facebook/campaign/123456789/ads?accessToken={TOKEN}` },
    ],
  },

  // ── 15. AD SET ───────────────────────────────────────────────────────────
  {
    id: 'adset', num: '15', name: 'Ad Set', group: 'Advertising',
    endpoints: [
      { id: 'adset-create', label: 'Create Ad Set', method: 'POST', url: `${B}/api/facebook/adset?accessToken={TOKEN}`, body: '{"campaignId":"123456789","accountId":"act_123456789","name":"Test AdSet","dailyBudget":500,"bidAmount":100,"billingEvent":"IMPRESSIONS","optimizationGoal":"REACH","targeting":{}}' },
      { id: 'adset-get', label: 'Get Ad Set', method: 'GET', url: `${B}/api/facebook/adset/123456789?accessToken={TOKEN}` },
      { id: 'adset-list', label: 'List Ad Sets', method: 'POST', url: `${B}/api/facebook/adset/list?accessToken={TOKEN}`, body: '{"campaignId":"123456789","limit":10}' },
      { id: 'adset-update', label: 'Update Ad Set', method: 'PUT', url: `${B}/api/facebook/adset/123456789?accessToken={TOKEN}`, body: '{"adSetId":"123456789","name":"Updated AdSet","status":"ACTIVE"}' },
      { id: 'adset-delete', label: 'Delete Ad Set', method: 'DELETE', url: `${B}/api/facebook/adset/123456789?accessToken={TOKEN}` },
      { id: 'adset-ads', label: 'Get Ads', method: 'GET', url: `${B}/api/facebook/adset/123456789/ads?accessToken={TOKEN}` },
    ],
  },

  // ── 16. AD ───────────────────────────────────────────────────────────────
  {
    id: 'ad', num: '16', name: 'Ad', group: 'Advertising',
    endpoints: [
      { id: 'ad-create', label: 'Create Ad', method: 'POST', url: `${B}/api/facebook/ad?accessToken={TOKEN}`, body: '{"adSetId":"123456789","accountId":"act_123456789","name":"Test Ad","status":"PAUSED","creative":{}}' },
      { id: 'ad-get', label: 'Get Ad', method: 'GET', url: `${B}/api/facebook/ad/123456789?accessToken={TOKEN}` },
      { id: 'ad-list', label: 'List Ads', method: 'POST', url: `${B}/api/facebook/ad/list?accessToken={TOKEN}`, body: '{"adSetId":"123456789","limit":10}' },
      { id: 'ad-update', label: 'Update Ad', method: 'PUT', url: `${B}/api/facebook/ad/123456789?accessToken={TOKEN}`, body: '{"adId":"123456789","name":"Updated Ad","status":"ACTIVE"}' },
      { id: 'ad-delete', label: 'Delete Ad', method: 'DELETE', url: `${B}/api/facebook/ad/123456789?accessToken={TOKEN}` },
      { id: 'ad-creative', label: 'Create Ad Creative', method: 'POST', url: `${B}/api/facebook/ad/creative?accessToken={TOKEN}`, body: '{"accountId":"act_123456789","name":"Test Creative","objectStorySpec":{}}' },
      { id: 'ad-preview', label: 'Get Ad Preview', method: 'GET', url: `${B}/api/facebook/ad/123456789/preview?adFormat=DESKTOP_FEED_STANDARD&accessToken={TOKEN}` },
      { id: 'ad-creatives', label: 'Get Account Creatives', method: 'GET', url: `${B}/api/facebook/ad/account/act_123456789/creatives?accessToken={TOKEN}` },
    ],
  },

  // ── 17. PIXEL ────────────────────────────────────────────────────────────
  {
    id: 'pixel', num: '17', name: 'Pixel', group: 'Analytics',
    endpoints: [
      { id: 'pixel-create', label: 'Create Pixel', method: 'POST', url: `${B}/api/facebook/pixel/account/act_123456789?accessToken={TOKEN}`, body: '{"name":"My Test Pixel"}' },
      { id: 'pixel-list', label: 'Get Account Pixels', method: 'GET', url: `${B}/api/facebook/pixel/account/act_123456789?accessToken={TOKEN}` },
      { id: 'pixel-get', label: 'Get Pixel', method: 'GET', url: `${B}/api/facebook/pixel/123456789?accessToken={TOKEN}` },
      { id: 'pixel-update', label: 'Update Pixel', method: 'PUT', url: `${B}/api/facebook/pixel/123456789?accessToken={TOKEN}`, body: '{"name":"Updated Pixel"}' },
      { id: 'pixel-stats', label: 'Get Pixel Stats', method: 'GET', url: `${B}/api/facebook/pixel/123456789/stats?accessToken={TOKEN}` },
      { id: 'pixel-audiences', label: 'Get Pixel Audiences', method: 'GET', url: `${B}/api/facebook/pixel/123456789/audiences?accessToken={TOKEN}` },
      { id: 'pixel-share', label: 'Share Pixel', method: 'POST', url: `${B}/api/facebook/pixel/123456789/share?accessToken={TOKEN}`, body: '{"targetAdAccountId":"act_123456789"}' },
      { id: 'pixel-diagnostics', label: 'Get Diagnostics', method: 'GET', url: `${B}/api/facebook/pixel/123456789/diagnostics?accessToken={TOKEN}` },
    ],
  },

  // ── 18. CONVERSIONS API ──────────────────────────────────────────────────
  {
    id: 'capi', num: '18', name: 'Conversions API', group: 'Analytics',
    endpoints: [
      { id: 'capi-events', label: 'Send Events', method: 'POST', url: `${B}/api/facebook/capi/events`, body: '{"PixelId":"123456789","AccessToken":"{TOKEN}","Data":[{"EventName":"Purchase","EventTime":1700000000}]}' },
      { id: 'capi-purchase', label: 'Send Purchase Event', method: 'POST', url: `${B}/api/facebook/capi/events/purchase`, body: '{"PixelId":"123456789","AccessToken":"{TOKEN}","EventTime":1700000000,"Value":29.99,"Currency":"USD"}' },
      { id: 'capi-add-to-cart', label: 'Send Add to Cart Event', method: 'POST', url: `${B}/api/facebook/capi/events/add-to-cart`, body: '{"PixelId":"123456789","AccessToken":"{TOKEN}","EventTime":1700000000,"Value":50.0,"Currency":"USD"}' },
      { id: 'capi-page-view', label: 'Send Page View Event', method: 'POST', url: `${B}/api/facebook/capi/events/page-view`, body: '{"PixelId":"123456789","AccessToken":"{TOKEN}","EventTime":1700000000,"EventSourceUrl":"https://yoursite.com"}' },
      { id: 'capi-lead', label: 'Send Lead Event', method: 'POST', url: `${B}/api/facebook/capi/events/lead`, body: '{"PixelId":"123456789","AccessToken":"{TOKEN}","EventTime":1700000000,"Email":"test@test.com"}' },
    ],
  },

  // ── 19. CATALOG ──────────────────────────────────────────────────────────
  {
    id: 'catalog', num: '19', name: 'Catalog', group: 'Catalog & Commerce',
    endpoints: [
      { id: 'cat-create', label: 'Create Catalog', method: 'POST', url: `${B}/api/facebook/catalog?accessToken={TOKEN}`, body: '{"businessId":"{BIZ_ID}","name":"Test Catalog","verticalType":"commerce"}' },
      { id: 'cat-get', label: 'Get Catalog', method: 'GET', url: `${B}/api/facebook/catalog/123456789?accessToken={TOKEN}` },
      { id: 'cat-list', label: 'List Catalogs', method: 'POST', url: `${B}/api/facebook/catalog/list?accessToken={TOKEN}`, body: '{"businessId":"{BIZ_ID}"}' },
      { id: 'cat-delete', label: 'Delete Catalog', method: 'DELETE', url: `${B}/api/facebook/catalog/123456789?accessToken={TOKEN}` },
      { id: 'cat-add-product', label: 'Add Product', method: 'POST', url: `${B}/api/facebook/catalog/123456789/product?accessToken={TOKEN}`, body: '{"catalogId":"123456789","retailerId":"SKU001","name":"Test Product","description":"Product description","price":100,"currency":"USD","url":"https://example.com/product","imageUrl":"https://example.com/img.jpg","availability":"in stock","condition":"new","brand":"TestBrand"}' },
      { id: 'cat-get-product', label: 'Get Product', method: 'GET', url: `${B}/api/facebook/catalog/product/prod123?accessToken={TOKEN}` },
      { id: 'cat-list-products', label: 'List Products', method: 'POST', url: `${B}/api/facebook/catalog/123456789/products?accessToken={TOKEN}`, body: '{"catalogId":"123456789","limit":25}' },
      { id: 'cat-update-product', label: 'Update Product', method: 'PUT', url: `${B}/api/facebook/catalog/product/prod123?accessToken={TOKEN}`, body: '{"productId":"prod123","price":200,"availability":"out of stock"}' },
      { id: 'cat-delete-product', label: 'Delete Product', method: 'DELETE', url: `${B}/api/facebook/catalog/product/prod123?accessToken={TOKEN}` },
      { id: 'cat-product-set', label: 'Create Product Set', method: 'POST', url: `${B}/api/facebook/catalog/123456789/product-set?accessToken={TOKEN}`, body: '{"catalogId":"123456789","name":"Test Set","filter":{}}' },
      { id: 'cat-product-sets', label: 'Get Product Sets', method: 'GET', url: `${B}/api/facebook/catalog/123456789/product-sets?accessToken={TOKEN}` },
    ],
  },

  // ── 20. INSTAGRAM ────────────────────────────────────────────────────────
  {
    id: 'instagram', num: '20', name: 'Instagram', group: 'Instagram',
    endpoints: [
      { id: 'ig-account', label: 'Get IG Account', method: 'GET', url: `${B}/api/facebook/instagram/123456789/account?accessToken={TOKEN}` },
      { id: 'ig-page-ig', label: 'Get Page Instagram', method: 'GET', url: `${B}/api/facebook/instagram/page/{PAGE_ID}/instagram?accessToken={TOKEN}` },
      { id: 'ig-media-create', label: 'Create Media Container', method: 'POST', url: `${B}/api/facebook/instagram/media?accessToken={TOKEN}`, body: '{"igUserId":"123456789","imageUrl":"https://example.com/img.jpg","caption":"Test caption"}' },
      { id: 'ig-media-publish', label: 'Publish Media', method: 'POST', url: `${B}/api/facebook/instagram/media/publish?accessToken={TOKEN}`, body: '{"igUserId":"123456789","creationId":"creationId"}' },
      { id: 'ig-media-list', label: 'List Media', method: 'POST', url: `${B}/api/facebook/instagram/media/list?accessToken={TOKEN}`, body: '{"igUserId":"123456789","limit":10}' },
      { id: 'ig-media-insights', label: 'Media Insights', method: 'POST', url: `${B}/api/facebook/instagram/media/insights?accessToken={TOKEN}`, body: '{"mediaId":"123456789","metric":["impressions","reach","likes"]}' },
      { id: 'ig-hashtag-search', label: 'Hashtag Search', method: 'POST', url: `${B}/api/facebook/instagram/hashtag/search?accessToken={TOKEN}`, body: '{"igUserId":"123456789","hashtagName":"sunset"}' },
      { id: 'ig-hashtag-media', label: 'Hashtag Media', method: 'POST', url: `${B}/api/facebook/instagram/hashtag/media?accessToken={TOKEN}`, body: '{"igUserId":"123456789","hashtagId":"hashtagId","type":"top_media"}' },
      { id: 'ig-media-comments', label: 'Get Media Comments', method: 'GET', url: `${B}/api/facebook/instagram/media/123456789/comments?accessToken={TOKEN}` },
      { id: 'ig-comment-reply', label: 'Reply to Comment', method: 'POST', url: `${B}/api/facebook/instagram/comment/123456789/reply?message=Hello&accessToken={TOKEN}` },
      { id: 'ig-comment-delete', label: 'Delete Comment', method: 'DELETE', url: `${B}/api/facebook/instagram/comment/123456789?accessToken={TOKEN}` },
      { id: 'ig-biz-discovery', label: 'Business Discovery', method: 'POST', url: `${B}/api/facebook/instagram/business-discovery?accessToken={TOKEN}`, body: '{"igUserId":"123456789","username":"testuser","accessToken":"{TOKEN}"}' },
      { id: 'ig-insights', label: 'Account Insights', method: 'GET', url: `${B}/api/facebook/instagram/123456789/insights?metrics=follower_count&period=day&accessToken={TOKEN}` },
      { id: 'ig-story', label: 'Create Story', method: 'POST', url: `${B}/api/facebook/instagram/story`, body: '{"igUserId":"123456789","imageUrl":"https://example.com/img.jpg","accessToken":"{TOKEN}"}' },
      { id: 'ig-reel', label: 'Create Reel', method: 'POST', url: `${B}/api/facebook/instagram/reel`, body: '{"igUserId":"123456789","videoUrl":"https://example.com/vid.mp4","caption":"Test reel","accessToken":"{TOKEN}"}' },
      { id: 'ig-carousel', label: 'Create Carousel', method: 'POST', url: `${B}/api/facebook/instagram/carousel`, body: '{"igUserId":"123456789","items":[{"imageUrl":"https://example.com/img.jpg","isCarouselItem":true}],"caption":"Test carousel","accessToken":"{TOKEN}"}' },
      { id: 'ig-container-status', label: 'Container Status', method: 'POST', url: `${B}/api/facebook/instagram/container/status`, body: '{"containerId":"containerId","accessToken":"{TOKEN}"}' },
      { id: 'ig-publishing-limit', label: 'Publishing Limit', method: 'POST', url: `${B}/api/facebook/instagram/publishing-limit`, body: '{"igUserId":"123456789","accessToken":"{TOKEN}"}' },
      { id: 'ig-mentions', label: 'Get Mentions', method: 'POST', url: `${B}/api/facebook/instagram/mentions`, body: '{"igUserId":"123456789","limit":10,"accessToken":"{TOKEN}"}' },
      { id: 'ig-tagged-media', label: 'Get Tagged Media', method: 'POST', url: `${B}/api/facebook/instagram/tagged-media`, body: '{"igUserId":"123456789","limit":10,"accessToken":"{TOKEN}"}' },
      { id: 'ig-dm-send', label: 'Send DM', method: 'POST', url: `${B}/api/facebook/instagram/dm/send`, body: '{"igUserId":"123456789","recipientIgId":"recipientId","message":"Hello!","accessToken":"{TOKEN}"}' },
      { id: 'ig-dm-convos', label: 'Get DM Conversations', method: 'POST', url: `${B}/api/facebook/instagram/dm/conversations`, body: '{"igUserId":"123456789","limit":10,"accessToken":"{TOKEN}"}' },
      { id: 'ig-stories', label: 'Get Stories', method: 'GET', url: `${B}/api/facebook/instagram/123456789/stories?accessToken={TOKEN}` },
      { id: 'ig-live-media', label: 'Get Live Media', method: 'GET', url: `${B}/api/facebook/instagram/123456789/live-media?accessToken={TOKEN}` },
      { id: 'ig-media-children', label: 'Get Media Children', method: 'GET', url: `${B}/api/facebook/instagram/media/123456789/children?accessToken={TOKEN}` },
      { id: 'ig-media-delete', label: 'Delete Media', method: 'DELETE', url: `${B}/api/facebook/instagram/media/123456789?accessToken={TOKEN}` },
      { id: 'ig-toggle-comments', label: 'Toggle Comments', method: 'POST', url: `${B}/api/facebook/instagram/media/123456789/toggle-comments?commentsEnabled=true&accessToken={TOKEN}` },
      { id: 'ig-recent-hashtags', label: 'Recently Searched Hashtags', method: 'GET', url: `${B}/api/facebook/instagram/123456789/recently-searched-hashtags?accessToken={TOKEN}` },
      { id: 'ig-mentioned-comment', label: 'Get Mentioned Comment', method: 'GET', url: `${B}/api/facebook/instagram/123456789/mentioned-comment?commentId=commentId&accessToken={TOKEN}` },
      { id: 'ig-mentioned-media', label: 'Get Mentioned Media', method: 'GET', url: `${B}/api/facebook/instagram/123456789/mentioned-media?mediaId=mediaId&accessToken={TOKEN}` },
      { id: 'ig-media-detail', label: 'Get Media Detail', method: 'GET', url: `${B}/api/facebook/instagram/media/123456789/detail?accessToken={TOKEN}` },
      { id: 'ig-comment-hide', label: 'Hide Comment', method: 'POST', url: `${B}/api/facebook/instagram/comment/123456789/hide?hide=true&accessToken={TOKEN}` },
      { id: 'ig-comment-replies', label: 'Get Comment Replies', method: 'GET', url: `${B}/api/facebook/instagram/comment/123456789/replies?accessToken={TOKEN}` },
      { id: 'ig-comment-get', label: 'Get Comment', method: 'GET', url: `${B}/api/facebook/instagram/comment/123456789?accessToken={TOKEN}` },
      { id: 'ig-media-comment', label: 'Post Comment on Media', method: 'POST', url: `${B}/api/facebook/instagram/media/123456789/comment`, body: '{"mediaId":"123456789","message":"Great post!","accessToken":"{TOKEN}"}' },
      { id: 'ig-biz-disc-media', label: 'Business Discovery Media', method: 'POST', url: `${B}/api/facebook/instagram/business-discovery/media`, body: '{"igUserId":"123456789","username":"testuser","limit":10,"accessToken":"{TOKEN}"}' },
      { id: 'ig-webhook-sub', label: 'Subscribe Webhook', method: 'POST', url: `${B}/api/facebook/instagram/webhook/subscribe`, body: '{"appId":"appId","igUserId":"123456789","callbackUrl":"https://example.com/webhook","verifyToken":"secret","accessToken":"{TOKEN}"}' },
      { id: 'ig-conv-messages', label: 'Get Conversation Messages', method: 'POST', url: `${B}/api/facebook/instagram/conversation/messages`, body: '{"igUserId":"123456789","conversationId":"conversationId","accessToken":"{TOKEN}"}' },
    ],
  },

  // ── 21. INSTAGRAM SHOPPING ───────────────────────────────────────────────
  {
    id: 'ig-shopping', num: '21', name: 'Instagram Shopping', group: 'Instagram',
    endpoints: [
      { id: 'igs-catalog', label: 'Get IG Catalog', method: 'GET', url: `${B}/api/facebook/instagram-shopping/123456789/catalog?accessToken={TOKEN}` },
      { id: 'igs-product-tags-add', label: 'Add Product Tags', method: 'POST', url: `${B}/api/facebook/instagram-shopping/media/123456789/product-tags?accessToken={TOKEN}`, body: '{"tags":[{"productId":"123","x":0.5,"y":0.5}]}' },
      { id: 'igs-product-tags-get', label: 'Get Product Tags', method: 'GET', url: `${B}/api/facebook/instagram-shopping/media/123456789/product-tags?accessToken={TOKEN}` },
      { id: 'igs-product-tags-del', label: 'Delete Product Tags', method: 'DELETE', url: `${B}/api/facebook/instagram-shopping/media/123456789/product-tags?accessToken={TOKEN}` },
      { id: 'igs-tagged-media', label: 'Get Tagged Media', method: 'GET', url: `${B}/api/facebook/instagram-shopping/123456789/tagged-media?accessToken={TOKEN}` },
      { id: 'igs-appeal', label: 'Appeal Product', method: 'POST', url: `${B}/api/facebook/instagram-shopping/123456789/appeal?accessToken={TOKEN}`, body: '{"productId":"productId","appealReason":"Product correctly represents my account"}' },
      { id: 'igs-eligibility', label: 'Get Eligibility', method: 'GET', url: `${B}/api/facebook/instagram-shopping/123456789/eligibility?accessToken={TOKEN}` },
      { id: 'igs-catalog-products', label: 'Get Catalog Products', method: 'GET', url: `${B}/api/facebook/instagram-shopping/catalog/123456789/products?accessToken={TOKEN}` },
      { id: 'igs-shops', label: 'Get IG Shops', method: 'GET', url: `${B}/api/facebook/instagram-shopping/123456789/shops?accessToken={TOKEN}` },
      { id: 'igs-avail-catalogs', label: 'Get Available Catalogs', method: 'GET', url: `${B}/api/facebook/instagram-shopping/123456789/available-catalogs?accessToken={TOKEN}` },
    ],
  },

  // ── 22. LIVE VIDEO ───────────────────────────────────────────────────────
  {
    id: 'live', num: '22', name: 'Live Video', group: 'Messaging & Social',
    endpoints: [
      { id: 'live-create', label: 'Create Live Video', method: 'POST', url: `${B}/api/facebook/live/{PAGE_ID}?accessToken={TOKEN}`, body: '{"title":"Test Live","description":"Live stream description","privacy":"EVERYONE"}' },
      { id: 'live-page-videos', label: 'Get Page Live Videos', method: 'GET', url: `${B}/api/facebook/live/{PAGE_ID}/videos?accessToken={TOKEN}` },
      { id: 'live-get', label: 'Get Live Video', method: 'GET', url: `${B}/api/facebook/live/video/123456789?accessToken={TOKEN}` },
      { id: 'live-update', label: 'Update Live Video', method: 'PUT', url: `${B}/api/facebook/live/video/123456789?accessToken={TOKEN}`, body: '{"title":"Updated Live Title"}' },
      { id: 'live-end', label: 'End Live Video', method: 'POST', url: `${B}/api/facebook/live/video/123456789/end?accessToken={TOKEN}` },
      { id: 'live-delete', label: 'Delete Live Video', method: 'DELETE', url: `${B}/api/facebook/live/video/123456789?accessToken={TOKEN}` },
      { id: 'live-errors', label: 'Get Live Errors', method: 'GET', url: `${B}/api/facebook/live/video/123456789/errors?accessToken={TOKEN}` },
      { id: 'live-blocked', label: 'Get Blocked Users', method: 'GET', url: `${B}/api/facebook/live/video/123456789/blocked-users?accessToken={TOKEN}` },
    ],
  },

  // ── 23. MESSENGER ────────────────────────────────────────────────────────
  {
    id: 'messenger', num: '23', name: 'Messenger', group: 'Messaging & Social',
    endpoints: [
      { id: 'msg-send', label: 'Send Message', method: 'POST', url: `${B}/api/facebook/messenger/send?pageAccessToken={TOKEN}`, body: '{"recipientId":"PSID123","messageText":"Hello from ApiToolKit!"}' },
      { id: 'msg-conversations', label: 'Get Conversations', method: 'POST', url: `${B}/api/facebook/messenger/conversations?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","limit":25}' },
      { id: 'msg-messages', label: 'Get Messages', method: 'POST', url: `${B}/api/facebook/messenger/messages?accessToken={TOKEN}`, body: '{"conversationId":"conversationId","limit":25}' },
      { id: 'msg-user-profile', label: 'Get User Profile', method: 'POST', url: `${B}/api/facebook/messenger/user/profile?pageAccessToken={TOKEN}`, body: '{"psid":"PSID123","pageId":"{PAGE_ID}"}' },
      { id: 'msg-mark-seen', label: 'Mark Seen', method: 'POST', url: `${B}/api/facebook/messenger/mark-seen?pageAccessToken={TOKEN}`, body: '{"recipientId":"PSID123"}' },
      { id: 'msg-typing', label: 'Send Typing Action', method: 'POST', url: `${B}/api/facebook/messenger/typing?pageAccessToken={TOKEN}`, body: '{"recipientId":"PSID123","action":"typing_on"}' },
      { id: 'msg-subscribe', label: 'Subscribe Messenger', method: 'POST', url: `${B}/api/facebook/messenger/subscribe?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","pageAccessToken":"{TOKEN}"}' },
      { id: 'msg-subscriptions', label: 'Get Subscriptions', method: 'GET', url: `${B}/api/facebook/messenger/page/{PAGE_ID}/subscriptions?accessToken={TOKEN}` },
      { id: 'msg-page-profile', label: 'Set Page Profile', method: 'POST', url: `${B}/api/facebook/messenger/page/{PAGE_ID}/profile?accessToken={TOKEN}`, body: '{"greeting":"Hello! Welcome to our page.","getStartedPayload":"GET_STARTED"}' },
    ],
  },

  // ── 24. THREADS ──────────────────────────────────────────────────────────
  {
    id: 'threads', num: '24', name: 'Threads', group: 'Messaging & Social',
    endpoints: [
      { id: 'th-profile', label: 'Get Profile', method: 'GET', url: `${B}/api/facebook/threads/profile?accessToken={TOKEN}` },
      { id: 'th-posts', label: 'Get Posts', method: 'GET', url: `${B}/api/facebook/threads/posts?accessToken={TOKEN}` },
      { id: 'th-text', label: 'Create Text Post', method: 'POST', url: `${B}/api/facebook/threads/posts/text?accessToken={TOKEN}`, body: '{"text":"Hello Threads from ApiToolKit!"}' },
      { id: 'th-image', label: 'Create Image Post', method: 'POST', url: `${B}/api/facebook/threads/posts/image?accessToken={TOKEN}`, body: '{"mediaUrl":"https://example.com/img.jpg","mediaType":"IMAGE","text":"Image caption"}' },
      { id: 'th-video', label: 'Create Video Post', method: 'POST', url: `${B}/api/facebook/threads/posts/video?accessToken={TOKEN}`, body: '{"mediaUrl":"https://example.com/vid.mp4","mediaType":"VIDEO","text":"Video caption"}' },
      { id: 'th-carousel', label: 'Create Carousel Post', method: 'POST', url: `${B}/api/facebook/threads/posts/carousel?accessToken={TOKEN}`, body: '{"mediaUrls":["https://example.com/img1.jpg","https://example.com/img2.jpg"],"text":"Carousel caption"}' },
      { id: 'th-publish', label: 'Publish Post', method: 'POST', url: `${B}/api/facebook/threads/posts/publish?accessToken={TOKEN}`, body: '{"creationId":"creationId"}' },
      { id: 'th-get', label: 'Get Thread', method: 'GET', url: `${B}/api/facebook/threads/posts/123456789?accessToken={TOKEN}` },
      { id: 'th-delete', label: 'Delete Thread', method: 'DELETE', url: `${B}/api/facebook/threads/posts/123456789?accessToken={TOKEN}` },
      { id: 'th-reply', label: 'Reply to Thread', method: 'POST', url: `${B}/api/facebook/threads/posts/123456789/reply?accessToken={TOKEN}`, body: '{"text":"my reply"}' },
      { id: 'th-replies', label: 'Get Replies', method: 'GET', url: `${B}/api/facebook/threads/posts/123456789/replies?accessToken={TOKEN}` },
      { id: 'th-conversation', label: 'Get Conversation', method: 'GET', url: `${B}/api/facebook/threads/posts/123456789/conversation?accessToken={TOKEN}` },
      { id: 'th-insights', label: 'Get Account Insights', method: 'GET', url: `${B}/api/facebook/threads/insights?accessToken={TOKEN}` },
      { id: 'th-post-insights', label: 'Get Post Insights', method: 'GET', url: `${B}/api/facebook/threads/posts/123456789/insights?accessToken={TOKEN}` },
      { id: 'th-pub-limit', label: 'Get Publishing Limit', method: 'GET', url: `${B}/api/facebook/threads/publishing-limit?accessToken={TOKEN}` },
    ],
  },

  // ── 25. WEBHOOK ──────────────────────────────────────────────────────────
  {
    id: 'webhook', num: '25', name: 'Webhook', group: 'Developer',
    endpoints: [
      { id: 'wh-subscribe', label: 'Subscribe Webhook', method: 'POST', url: `${B}/api/facebook/webhook/subscribe?accessToken={TOKEN}`, body: '{"objectType":"page","callbackUrl":"https://example.com/webhook","verifyToken":"secret","fields":["feed","messages"],"appId":"appId"}' },
      { id: 'wh-subscriptions', label: 'Get Subscriptions', method: 'POST', url: `${B}/api/facebook/webhook/subscriptions?accessToken={TOKEN}`, body: '{"appId":"appId"}' },
      { id: 'wh-verify', label: 'Verify Webhook', method: 'GET', url: `${B}/api/facebook/webhook/verify?hub.mode=subscribe&hub.verify_token=secret&hub.challenge=CHALLENGE123` },
      { id: 'wh-payload', label: 'Send Test Payload', method: 'POST', url: `${B}/api/facebook/webhook/payload`, body: '{"object":"page","entry":[]}' },
    ],
  },

  // ── 26. COMMERCE ─────────────────────────────────────────────────────────
  {
    id: 'commerce', num: '26', name: 'Commerce', group: 'Catalog & Commerce',
    endpoints: [
      { id: 'cm-shops', label: 'Get Shops', method: 'GET', url: `${B}/api/facebook/commerce/shops/{PAGE_ID}?accessToken={TOKEN}` },
      { id: 'cm-shop', label: 'Get Shop Detail', method: 'GET', url: `${B}/api/facebook/commerce/shops/detail/shopId?accessToken={TOKEN}` },
      { id: 'cm-orders', label: 'Get Orders', method: 'GET', url: `${B}/api/facebook/commerce/orders/{PAGE_ID}?accessToken={TOKEN}` },
      { id: 'cm-order', label: 'Get Order Detail', method: 'GET', url: `${B}/api/facebook/commerce/orders/detail/orderId?accessToken={TOKEN}` },
      { id: 'cm-acknowledge', label: 'Acknowledge Order', method: 'POST', url: `${B}/api/facebook/commerce/orders/orderId/acknowledge?accessToken={TOKEN}`, body: '{"merchantOrderReference":"REF-ORD-123456"}' },
      { id: 'cm-shipment', label: 'Add Shipment', method: 'POST', url: `${B}/api/facebook/commerce/orders/orderId/shipments?accessToken={TOKEN}`, body: '{"trackingNumber":"TRK123","carrierName":"UPS","shippingMethod":"STANDARD"}' },
      { id: 'cm-cancel', label: 'Cancel Order', method: 'POST', url: `${B}/api/facebook/commerce/orders/orderId/cancel?accessToken={TOKEN}`, body: '{"reasonCode":"CUSTOMER_REQUESTED"}' },
      { id: 'cm-refund', label: 'Refund Order', method: 'POST', url: `${B}/api/facebook/commerce/orders/orderId/refund?accessToken={TOKEN}`, body: '{"refundAmount":10.00,"currency":"USD","reasonText":"Customer return"}' },
      { id: 'cm-fulfillments', label: 'Get Fulfillments', method: 'GET', url: `${B}/api/facebook/commerce/orders/orderId/fulfillments?accessToken={TOKEN}` },
      { id: 'cm-returns', label: 'Get Returns', method: 'GET', url: `${B}/api/facebook/commerce/orders/orderId/returns?accessToken={TOKEN}` },
      { id: 'cm-account', label: 'Get Commerce Account', method: 'GET', url: `${B}/api/facebook/commerce/account/{PAGE_ID}?accessToken={TOKEN}` },
      { id: 'cm-settings', label: 'Get Commerce Settings', method: 'GET', url: `${B}/api/facebook/commerce/account/acctId/settings?accessToken={TOKEN}` },
      { id: 'cm-tax-get', label: 'Get Tax Settings', method: 'GET', url: `${B}/api/facebook/commerce/account/acctId/tax-settings?accessToken={TOKEN}` },
      { id: 'cm-tax-update', label: 'Update Tax Settings', method: 'PUT', url: `${B}/api/facebook/commerce/account/acctId/tax-settings?accessToken={TOKEN}`, body: '{"disablePaymentTax":false}' },
      { id: 'cm-return-get', label: 'Get Return Settings', method: 'GET', url: `${B}/api/facebook/commerce/account/acctId/return-settings?accessToken={TOKEN}` },
      { id: 'cm-return-update', label: 'Update Return Settings', method: 'PUT', url: `${B}/api/facebook/commerce/account/acctId/return-settings?accessToken={TOKEN}`, body: '{"returnWindowDays":30,"isEnabled":true}' },
      { id: 'cm-shipping', label: 'Get Shipping Profiles', method: 'GET', url: `${B}/api/facebook/commerce/account/acctId/shipping-profiles?accessToken={TOKEN}` },
      { id: 'cm-shop-collections', label: 'Get Shop Collections', method: 'GET', url: `${B}/api/facebook/commerce/shops/shopId/collections?accessToken={TOKEN}` },
      { id: 'cm-shop-create', label: 'Create Shop', method: 'POST', url: `${B}/api/facebook/commerce/shops/create?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","catalogId":"catalogId","name":"My Shop","description":"Shop description"}' },
      { id: 'cm-shop-update', label: 'Update Shop', method: 'PUT', url: `${B}/api/facebook/commerce/shops/shopId/update?accessToken={TOKEN}`, body: '{"shopId":"shopId","name":"Updated Shop","isEnabled":true}' },
      { id: 'cm-catalogs-list', label: 'Get Connected Catalogs', method: 'GET', url: `${B}/api/facebook/commerce/account/acctId/catalogs?accessToken={TOKEN}` },
      { id: 'cm-catalog-connect', label: 'Connect Catalog', method: 'POST', url: `${B}/api/facebook/commerce/account/acctId/catalogs/connect?accessToken={TOKEN}`, body: '{"commerceAccountId":"acctId","catalogId":"catalogId"}' },
      { id: 'cm-catalog-disconnect', label: 'Disconnect Catalog', method: 'DELETE', url: `${B}/api/facebook/commerce/account/acctId/catalogs/catalogId?accessToken={TOKEN}` },
      { id: 'cm-collections', label: 'Get Collections', method: 'GET', url: `${B}/api/facebook/commerce/account/acctId/collections?accessToken={TOKEN}` },
      { id: 'cm-collection-create', label: 'Create Collection', method: 'POST', url: `${B}/api/facebook/commerce/account/acctId/collections?accessToken={TOKEN}`, body: '{"commerceAccountId":"acctId","name":"Summer Sale","description":"Summer collection"}' },
      { id: 'cm-collection-update', label: 'Update Collection', method: 'PUT', url: `${B}/api/facebook/commerce/collections/collId?accessToken={TOKEN}`, body: '{"collectionId":"collId","name":"Updated Collection"}' },
      { id: 'cm-collection-delete', label: 'Delete Collection', method: 'DELETE', url: `${B}/api/facebook/commerce/collections/collId?accessToken={TOKEN}` },
      { id: 'cm-coll-products', label: 'Get Collection Products', method: 'GET', url: `${B}/api/facebook/commerce/collections/collId/products?accessToken={TOKEN}` },
      { id: 'cm-coll-add-product', label: 'Add Product to Collection', method: 'POST', url: `${B}/api/facebook/commerce/collections/collId/products?accessToken={TOKEN}`, body: '{"collectionId":"collId","productId":"productId"}' },
      { id: 'cm-coll-remove-product', label: 'Remove Product from Collection', method: 'DELETE', url: `${B}/api/facebook/commerce/collections/collId/products?accessToken={TOKEN}`, body: '{"collectionId":"collId","productId":"productId"}' },
    ],
  },

  // ── 27. PRODUCT FEED ─────────────────────────────────────────────────────
  {
    id: 'product-feed', num: '27', name: 'Product Feed', group: 'Catalog & Commerce',
    endpoints: [
      { id: 'pf-create', label: 'Create Feed', method: 'POST', url: `${B}/api/facebook/product-feed/catalogId/feeds?accessToken={TOKEN}`, body: '{"name":"Test Feed","url":"https://example.com/feed.csv","schedule":"DAILY"}' },
      { id: 'pf-list', label: 'Get Feeds', method: 'GET', url: `${B}/api/facebook/product-feed/catalogId/feeds?accessToken={TOKEN}` },
      { id: 'pf-get', label: 'Get Feed', method: 'GET', url: `${B}/api/facebook/product-feed/feeds/feedId?accessToken={TOKEN}` },
      { id: 'pf-delete', label: 'Delete Feed', method: 'DELETE', url: `${B}/api/facebook/product-feed/feeds/feedId?accessToken={TOKEN}` },
      { id: 'pf-uploads', label: 'Get Feed Uploads', method: 'GET', url: `${B}/api/facebook/product-feed/feeds/feedId/uploads?accessToken={TOKEN}` },
      { id: 'pf-upload-get', label: 'Get Upload', method: 'GET', url: `${B}/api/facebook/product-feed/uploads/uploadId?accessToken={TOKEN}` },
      { id: 'pf-errors', label: 'Get Feed Errors', method: 'GET', url: `${B}/api/facebook/product-feed/feeds/feedId/errors?accessToken={TOKEN}` },
    ],
  },

  // ── 28. WHATSAPP ─────────────────────────────────────────────────────────
  {
    id: 'whatsapp', num: '28', name: 'WhatsApp', group: 'WhatsApp',
    endpoints: [
      { id: 'wa-text', label: 'Send Text', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/text`, body: '{"to":"919876543210","message":"Hello from ApiToolKit!"}' },
      { id: 'wa-template', label: 'Send Template', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/template`, body: '{"to":"919876543210","templateName":"hello_world","languageCode":"en_US"}' },
      { id: 'wa-image', label: 'Send Image', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/image`, body: '{"to":"919876543210","imageUrl":"https://example.com/img.jpg"}' },
      { id: 'wa-video', label: 'Send Video', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/video`, body: '{"to":"919876543210","videoUrl":"https://example.com/vid.mp4"}' },
      { id: 'wa-audio', label: 'Send Audio', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/audio`, body: '{"to":"919876543210","audioUrl":"https://example.com/aud.mp3"}' },
      { id: 'wa-document', label: 'Send Document', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/document`, body: '{"to":"919876543210","documentUrl":"https://example.com/doc.pdf","filename":"doc.pdf"}' },
      { id: 'wa-sticker', label: 'Send Sticker', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/sticker`, body: '{"to":"919876543210","stickerUrl":"https://example.com/sticker.webp"}' },
      { id: 'wa-location', label: 'Send Location', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/location`, body: '{"to":"919876543210","latitude":28.6,"longitude":77.2,"name":"New Delhi","address":"India"}' },
      { id: 'wa-contact', label: 'Send Contact', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/contact`, body: '{"to":"919876543210","contacts":[{"name":{"formattedName":"Test User"},"phones":[{"phone":"123456789","type":"CELL"}]}]}' },
      { id: 'wa-reaction', label: 'Send Reaction', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/reaction`, body: '{"to":"919876543210","messageId":"msg123","emoji":"👍"}' },
      { id: 'wa-buttons', label: 'Send Interactive Buttons', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/interactive/buttons`, body: '{"to":"919876543210","bodyText":"Choose an option:","buttons":[{"id":"btn1","title":"Yes"},{"id":"btn2","title":"No"}]}' },
      { id: 'wa-list', label: 'Send Interactive List', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/interactive/list`, body: '{"to":"919876543210","headerText":"Menu","bodyText":"Select an item:","buttonText":"View Options","sections":[{"title":"Section 1","rows":[{"id":"row1","title":"Option 1","description":"Description 1"}]}]}' },
      { id: 'wa-product', label: 'Send Product', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/interactive/product`, body: '{"to":"919876543210","catalogId":"cat1","productRetailerId":"prod1"}' },
      { id: 'wa-catalog', label: 'Send Catalog', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/interactive/catalog`, body: '{"to":"919876543210","catalogId":"cat1","bodyText":"Browse our products","sections":[{"title":"Products","productRetailerIds":["p1","p2"]}]}' },
      { id: 'wa-read', label: 'Mark Message Read', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/read`, body: '{"messageId":"msg123"}' },
      { id: 'wa-location-req', label: 'Request Location', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/interactive/location-request`, body: '{"to":"919876543210","bodyText":"Please share your location"}' },
      { id: 'wa-cta-url', label: 'Send CTA URL Button', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/interactive/cta-url`, body: '{"to":"919876543210","headerText":"Visit Us","bodyText":"Click to visit","buttonDisplayText":"Open Website","buttonUrl":"https://example.com"}' },
      { id: 'wa-flow', label: 'Send Flow', method: 'POST', url: `${B}/api/facebook/whatsapp/messages/interactive/flow`, body: '{"to":"919876543210","flowId":"flow1","flowToken":"token","bodyText":"Please fill the form"}' },
      { id: 'wa-media-upload', label: 'Upload Media', method: 'POST', url: `${B}/api/facebook/whatsapp/media/upload`, body: '{"url":"https://example.com/img.jpg","type":"image/jpeg"}' },
      { id: 'wa-media-get', label: 'Get Media', method: 'GET', url: `${B}/api/facebook/whatsapp/media/mediaId?accessToken={TOKEN}` },
      { id: 'wa-media-del', label: 'Delete Media', method: 'DELETE', url: `${B}/api/facebook/whatsapp/media/mediaId?accessToken={TOKEN}` },
      { id: 'wa-phones', label: 'Get Phone Numbers', method: 'GET', url: `${B}/api/facebook/whatsapp/phone-numbers?wabaId=waba123&accessToken={TOKEN}` },
      { id: 'wa-phone', label: 'Get Phone Number', method: 'GET', url: `${B}/api/facebook/whatsapp/phone-numbers/ph123?accessToken={TOKEN}` },
      { id: 'wa-req-code', label: 'Request Verification Code', method: 'POST', url: `${B}/api/facebook/whatsapp/phone-numbers/ph123/request-code`, body: '{"codeMethod":"SMS"}' },
      { id: 'wa-verify-code', label: 'Verify Code', method: 'POST', url: `${B}/api/facebook/whatsapp/phone-numbers/ph123/verify-code`, body: '{"code":"123456"}' },
      { id: 'wa-register', label: 'Register Phone', method: 'POST', url: `${B}/api/facebook/whatsapp/phone-numbers/ph123/register`, body: '{"pin":"123456"}' },
      { id: 'wa-deregister', label: 'Deregister Phone', method: 'POST', url: `${B}/api/facebook/whatsapp/phone-numbers/ph123/deregister`, body: '{}' },
      { id: 'wa-2fa', label: 'Set 2FA PIN', method: 'POST', url: `${B}/api/facebook/whatsapp/phone-numbers/ph123/2fa`, body: '{"pin":"123456"}' },
      { id: 'wa-biz-profile-get', label: 'Get Business Profile', method: 'GET', url: `${B}/api/facebook/whatsapp/business-profile?phoneNumberId=ph123&accessToken={TOKEN}` },
      { id: 'wa-biz-profile-set', label: 'Update Business Profile', method: 'POST', url: `${B}/api/facebook/whatsapp/business-profile`, body: '{"phoneNumberId":"ph123","about":"Test business description"}' },
      { id: 'wa-template-create', label: 'Create Template', method: 'POST', url: `${B}/api/facebook/whatsapp/templates`, body: '{"wabaId":"waba123","name":"test_template","language":"en_US","category":"UTILITY","components":[]}' },
      { id: 'wa-templates-list', label: 'Get Templates', method: 'GET', url: `${B}/api/facebook/whatsapp/templates?wabaId=waba123&accessToken={TOKEN}` },
      { id: 'wa-template-get', label: 'Get Template', method: 'GET', url: `${B}/api/facebook/whatsapp/templates/tmplId?accessToken={TOKEN}` },
      { id: 'wa-template-update', label: 'Update Template', method: 'PUT', url: `${B}/api/facebook/whatsapp/templates/tmplId`, body: '{"components":[]}' },
      { id: 'wa-template-delete', label: 'Delete Template', method: 'DELETE', url: `${B}/api/facebook/whatsapp/templates/tmplId?accessToken={TOKEN}` },
      { id: 'wa-qr-create', label: 'Create QR Code', method: 'POST', url: `${B}/api/facebook/whatsapp/qr-codes`, body: '{"phoneNumberId":"ph123","prefilledMessage":"Hi there!","imageFormat":"PNG"}' },
      { id: 'wa-qr-list', label: 'Get QR Codes', method: 'GET', url: `${B}/api/facebook/whatsapp/qr-codes?phoneNumberId=ph123&accessToken={TOKEN}` },
      { id: 'wa-qr-get', label: 'Get QR Code', method: 'GET', url: `${B}/api/facebook/whatsapp/qr-codes/qrId?phoneNumberId=ph123&accessToken={TOKEN}` },
      { id: 'wa-qr-update', label: 'Update QR Code', method: 'PUT', url: `${B}/api/facebook/whatsapp/qr-codes/qrId`, body: '{"prefilledMessage":"Updated message"}' },
      { id: 'wa-qr-delete', label: 'Delete QR Code', method: 'DELETE', url: `${B}/api/facebook/whatsapp/qr-codes/qrId?phoneNumberId=ph123&accessToken={TOKEN}` },
      { id: 'wa-wh-subscribe', label: 'Subscribe Webhook', method: 'POST', url: `${B}/api/facebook/whatsapp/webhook/subscribe`, body: '{"wabaId":"waba123","callbackUrl":"https://example.com/wh","verifyToken":"secret","fields":["messages"],"accessToken":"{TOKEN}"}' },
      { id: 'wa-wh-verify', label: 'Verify Webhook', method: 'GET', url: `${B}/api/facebook/whatsapp/webhook/verify?hub.mode=subscribe&hub.verify_token=secret&hub.challenge=CH123` },
      { id: 'wa-wh-payload', label: 'Send Webhook Payload', method: 'POST', url: `${B}/api/facebook/whatsapp/webhook/payload`, body: '{"object":"whatsapp_business_account","entry":[]}' },
      { id: 'wa-waba', label: 'Get WABA', method: 'GET', url: `${B}/api/facebook/whatsapp/waba/waba123?accessToken={TOKEN}` },
      { id: 'wa-analytics', label: 'Get Analytics', method: 'POST', url: `${B}/api/facebook/whatsapp/analytics`, body: '{"wabaId":"waba123","start":"1700000000","end":"1700086400","granularity":"DAY","accessToken":"{TOKEN}"}' },
      { id: 'wa-conv-analytics', label: 'Get Conversation Analytics', method: 'POST', url: `${B}/api/facebook/whatsapp/conversation-analytics`, body: '{"wabaId":"waba123","start":"1700000000","end":"1700086400","granularity":"DAY","accessToken":"{TOKEN}"}' },
    ],
  },

  // ── 29. WHATSAPP CATALOG ─────────────────────────────────────────────────
  {
    id: 'wa-catalog', num: '29', name: 'WhatsApp Catalog', group: 'WhatsApp',
    endpoints: [
      { id: 'wac-list', label: 'Get Catalogs', method: 'GET', url: `${B}/api/facebook/whatsapp/catalog/waba123/catalogs?accessToken={TOKEN}` },
      { id: 'wac-create', label: 'Create Catalog', method: 'POST', url: `${B}/api/facebook/whatsapp/catalog/waba123/catalogs?accessToken={TOKEN}`, body: '{"name":"WA Catalog","vertical":"commerce"}' },
      { id: 'wac-get', label: 'Get Catalog', method: 'GET', url: `${B}/api/facebook/whatsapp/catalog/catalogs/catId?accessToken={TOKEN}` },
      { id: 'wac-products', label: 'Get Products', method: 'GET', url: `${B}/api/facebook/whatsapp/catalog/catalogs/catId/products?accessToken={TOKEN}` },
      { id: 'wac-product-add', label: 'Add Product', method: 'POST', url: `${B}/api/facebook/whatsapp/catalog/catalogs/catId/products?accessToken={TOKEN}`, body: '{"name":"Test Product","description":"A great product","price":100,"currency":"USD","availability":"in stock","condition":"new"}' },
      { id: 'wac-product-get', label: 'Get Product', method: 'GET', url: `${B}/api/facebook/whatsapp/catalog/products/prodId?accessToken={TOKEN}` },
      { id: 'wac-product-del', label: 'Delete Product', method: 'DELETE', url: `${B}/api/facebook/whatsapp/catalog/products/prodId?accessToken={TOKEN}` },
    ],
  },

  // ── 30. LEAD GENERATION ──────────────────────────────────────────────────
  {
    id: 'lead-gen', num: '30', name: 'Lead Generation', group: 'Developer',
    endpoints: [
      { id: 'lg-forms', label: 'List Lead Gen Forms', method: 'POST', url: `${B}/api/facebook/leadgen/forms/list?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","limit":10}' },
      { id: 'lg-form-get', label: 'Get Form', method: 'GET', url: `${B}/api/facebook/leadgen/forms/formId?accessToken={TOKEN}` },
      { id: 'lg-form-create', label: 'Create Form', method: 'POST', url: `${B}/api/facebook/leadgen/forms?accessToken={TOKEN}`, body: '{"pageId":"{PAGE_ID}","name":"Test Form","questions":[{"type":"EMAIL"},{"type":"FULL_NAME"},{"type":"PHONE"}],"privacyPolicy":{"url":"https://example.com/privacy","linkType":"PRIVACY_POLICY"}}' },
      { id: 'lg-form-delete', label: 'Delete Form', method: 'DELETE', url: `${B}/api/facebook/leadgen/forms/formId?accessToken={TOKEN}` },
      { id: 'lg-leads', label: 'List Leads', method: 'POST', url: `${B}/api/facebook/leadgen/leads/list?accessToken={TOKEN}`, body: '{"formId":"formId","limit":10}' },
      { id: 'lg-lead-get', label: 'Get Lead', method: 'GET', url: `${B}/api/facebook/leadgen/leads/leadId?accessToken={TOKEN}` },
      { id: 'lg-lead-delete', label: 'Delete Lead', method: 'DELETE', url: `${B}/api/facebook/leadgen/leads/leadId?accessToken={TOKEN}` },
    ],
  },

  // ── 31. WHATSAPP FLOWS ───────────────────────────────────────────────────
  {
    id: 'wa-flows', num: '31', name: 'WhatsApp Flows', group: 'WhatsApp',
    endpoints: [
      { id: 'waf-list', label: 'List Flows', method: 'POST', url: `${B}/api/facebook/whatsapp/flows/list?accessToken={TOKEN}`, body: '{"wabaId":"waba123","limit":10}' },
      { id: 'waf-get', label: 'Get Flow', method: 'GET', url: `${B}/api/facebook/whatsapp/flows/flowId?accessToken={TOKEN}` },
      { id: 'waf-create', label: 'Create Flow', method: 'POST', url: `${B}/api/facebook/whatsapp/flows?accessToken={TOKEN}`, body: '{"wabaId":"waba123","name":"Test Flow","categories":["CUSTOMER_SERVICE"]}' },
      { id: 'waf-update', label: 'Update Flow', method: 'PUT', url: `${B}/api/facebook/whatsapp/flows/flowId?accessToken={TOKEN}`, body: '{"flowId":"flowId","name":"Updated Flow"}' },
      { id: 'waf-delete', label: 'Delete Flow', method: 'DELETE', url: `${B}/api/facebook/whatsapp/flows/flowId?accessToken={TOKEN}` },
      { id: 'waf-publish', label: 'Publish Flow', method: 'POST', url: `${B}/api/facebook/whatsapp/flows/flowId/publish?accessToken={TOKEN}` },
      { id: 'waf-deprecate', label: 'Deprecate Flow', method: 'POST', url: `${B}/api/facebook/whatsapp/flows/flowId/deprecate?accessToken={TOKEN}` },
      { id: 'waf-assets', label: 'Get Flow Assets', method: 'GET', url: `${B}/api/facebook/whatsapp/flows/flowId/assets?accessToken={TOKEN}` },
      { id: 'waf-upload', label: 'Upload Flow JSON', method: 'POST', url: `${B}/api/facebook/whatsapp/flows/flowId/assets?accessToken={TOKEN}`, body: '{"flowId":"flowId","flowJson":"{\"version\":\"5.0\",\"screens\":[]}"}' },
      { id: 'waf-preview', label: 'Get Flow Preview', method: 'GET', url: `${B}/api/facebook/whatsapp/flows/flowId/preview?accessToken={TOKEN}` },
    ],
  },
];

export const MODULE_GROUPS = [
  'Core',
  'Content',
  'Analytics',
  'Advertising',
  'Catalog & Commerce',
  'Instagram',
  'Messaging & Social',
  'WhatsApp',
  'Developer',
];

export function getModule(id: string): Module | undefined {
  return MODULES.find(m => m.id === id);
}
