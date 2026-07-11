export type Method = 'GET' | 'POST' | 'PUT' | 'DELETE';

export interface Endpoint {
  id: string;
  label: string;
  method: Method;
  url: string;   // template: {TOKEN}, {PAGE_TOKEN}, {USER_ID}, {BIZ_ID}, {PAGE_ID}
  body?: string; // default JSON body (may also contain {TOKEN} etc.)
}

export interface Module {
  id: string;
  num: string;
  name: string;
  group: string;
  endpoints: Endpoint[];
}

export interface AppState {
  apiBase: string;
  token: string;
  pageToken: string;
  userId: string;
  businessId: string;
  pageId: string;
  setToken: (v: string) => void;
  setPageToken: (v: string) => void;
  setUserId: (v: string) => void;
  setBusinessId: (v: string) => void;
  setPageId: (v: string) => void;
}
