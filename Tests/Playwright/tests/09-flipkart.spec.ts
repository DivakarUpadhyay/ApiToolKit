import { test, expect, APIRequestContext } from '@playwright/test';

const BASE = 'http://localhost:5052';

async function fget(request: APIRequestContext, path: string, params: Record<string, string> = {}) {
  const qs = new URLSearchParams(params).toString();
  const url = qs ? `${BASE}${path}?${qs}` : `${BASE}${path}`;
  return request.get(url);
}

async function fpost(request: APIRequestContext, path: string, body: unknown = {}) {
  return request.post(`${BASE}${path}`, { data: body });
}

async function fput(request: APIRequestContext, path: string, body: unknown = {}) {
  return request.put(`${BASE}${path}`, { data: body });
}

// ── 1. Listing Management ─────────────────────────────────────────────────────

test.describe('Flipkart - Listing APIs', () => {
  test('POST /api/flipkart/listings - create listing', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/listings', {
      Listings: {
        'SKU-001': {
          ProductId: 'PROD1234567890ABC',
          Price: { Mrp: 1000, SellingPrice: 800, Currency: 'INR' },
          Tax: { Hsn: '8471', TaxCode: 'GST_18' },
          ListingStatus: 'ACTIVE',
          FulfillmentProfile: 'NON_FBF',
          Fulfillment: { DispatchSla: 2, ShippingProvider: 'FLIPKART', ProcurementType: 'REGULAR' },
          Locations: [{ Id: 'loc-001', Status: 'ENABLED', Inventory: 10 }],
        },
      },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/flipkart/listings - update listing', async ({ request }) => {
    const res = await fput(request, '/api/flipkart/listings', {
      Listings: {
        'SKU-001': {
          ProductId: 'PROD1234567890ABC',
          Price: { Mrp: 1000, SellingPrice: 750, Currency: 'INR' },
          ListingStatus: 'ACTIVE',
          ArchivedStatus: 'NONE',
        },
      },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/flipkart/listings/{skuIds} - get by SKU', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/listings/SKU-001,SKU-002');
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/listings/details - get listing details', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/listings/details', {
      SkuIds: ['SKU-001', 'SKU-002'],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/listings/product/search - search listings', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/listings/product/search', {
      BatchNo: 1,
      InternalState: 'ACTIVE',
      SearchText: 'test product',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/listings/search - get all listings', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/listings/search', {
      ListingStatus: 'ACTIVE',
      PageId: null,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/listings/update/price - update price', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/listings/update/price', {
      Prices: {
        'SKU-001': {
          ProductId: 'PROD1234567890ABC',
          Price: { Mrp: 1000, SellingPrice: 700, Currency: 'INR' },
        },
      },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/listings/update/inventory - update inventory', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/listings/update/inventory', {
      Inventories: {
        'SKU-001': {
          ProductId: 'PROD1234567890ABC',
          Locations: [{ Id: 'loc-001', Inventory: 25 }],
        },
      },
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 2. HyperLocal Listing ─────────────────────────────────────────────────────

test.describe('Flipkart - HyperLocal Listing APIs', () => {
  test('POST /api/flipkart/hyperlocal - create hyperlocal listing', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/hyperlocal', {
      Listings: {
        'HL-SKU-001': {
          ProductId: 'PROD1234567890ABC',
          Price: { Mrp: 500, SellingPrice: 450, Currency: 'INR' },
          Tax: { Hsn: '8471', TaxCode: 'GST_12' },
          ListingStatus: 'ACTIVE',
          Package: { Name: 'Box 1', Dimensions: { Length: 10, Breadth: 8, Height: 5 }, Weight: 0.5 },
          Location: { Id: 'loc-001', Status: 'ENABLED', Inventory: 20 },
        },
      },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('PUT /api/flipkart/hyperlocal - update hyperlocal listing', async ({ request }) => {
    const res = await fput(request, '/api/flipkart/hyperlocal', {
      Listings: {
        'HL-SKU-001': {
          ProductId: 'PROD1234567890ABC',
          Price: { Mrp: 500, SellingPrice: 420, Currency: 'INR' },
          ArchivedStatus: 'NONE',
        },
      },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/flipkart/hyperlocal/{skuIds} - get hyperlocal listing', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/hyperlocal/HL-SKU-001');
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/hyperlocal/update/price - update HL price', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/hyperlocal/update/price', {
      Prices: {
        'HL-SKU-001': {
          ProductId: 'PROD1234567890ABC',
          Price: { Mrp: 500, SellingPrice: 400, Currency: 'INR' },
        },
      },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/hyperlocal/update/inventory - update HL inventory', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/hyperlocal/update/inventory', {
      Inventories: {
        'HL-SKU-001': {
          ProductId: 'PROD1234567890ABC',
          Locations: [{ Id: 'loc-001', Inventory: 15 }],
        },
      },
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 3. Order / Shipment Management ───────────────────────────────────────────

test.describe('Flipkart - Order / Shipment APIs', () => {
  test('POST /api/flipkart/orders/shipments/filter - filter shipments', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/orders/shipments/filter', {
      Filter: {
        Type: 'preDispatch',
        States: ['APPROVED', 'PACKED'],
        Hold: false,
        ShipmentTypes: ['NORMAL'],
      },
      Pagination: { PageSize: 10 },
      Sort: { Field: 'dispatchByDate', Order: 'asc' },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/flipkart/orders/shipments?shipmentIds= - get by shipment IDs', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/orders/shipments', { shipmentIds: 'SH001,SH002' });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/flipkart/orders/shipments/by-order-items - get by order item IDs', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/orders/shipments/by-order-items', { orderItemIds: 'OI001,OI002' });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/flipkart/orders/shipments/by-orders - get by order IDs', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/orders/shipments/by-orders', { orderIds: 'ORD001,ORD002' });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/orders/shipments/labels - generate labels', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/orders/shipments/labels', {
      Shipments: [
        {
          ShipmentId: 'SH001',
          LocationId: 'loc-001',
          TaxItems: [{ OrderItemId: 'OI001', PurchasePrice: 800, TaxRate: 18, Quantity: 1 }],
          Invoices: [{ OrderId: 'ORD001', InvoiceNumber: 'INV-001', InvoiceDate: '2026-07-09' }],
        },
      ],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/flipkart/orders/shipments/{ids}/labels - download labels', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/orders/shipments/SH001/labels');
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/orders/shipments/{ids}/label-only/pdf - download label PDF', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/orders/shipments/SH001/label-only/pdf');
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/orders/shipments/{ids}/label-only - download label bytes', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/orders/shipments/SH001/label-only');
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/flipkart/orders/shipments/{ids}/invoices - get invoices', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/orders/shipments/SH001/invoices');
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/orders/shipments/dispatch - dispatch shipments', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/orders/shipments/dispatch', {
      ShipmentIds: ['SH001', 'SH002'],
      LocationId: 'loc-001',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/flipkart/orders/shipments/{ids} - get shipment details', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/orders/shipments/SH001');
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/orders/shipments/cancel - cancel shipments', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/orders/shipments/cancel', {
      Shipments: [
        {
          ShipmentId: 'SH001',
          LocationId: 'loc-001',
          Reason: 'cannot_procure_item',
        },
      ],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/flipkart/orders/shipments/handover/counts - handover counts', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/orders/shipments/handover/counts', { locationId: 'loc-001' });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/orders/shipments/manifest - generate manifest', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/orders/shipments/manifest', {
      Params: [
        {
          VendorGroupCode: 'VG001',
          LocationId: 'loc-001',
          IsMps: false,
          PickUpDate: { From: '2026-07-09T00:00:00Z', To: '2026-07-09T23:59:59Z' },
        },
      ],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/orders/shipments/self-ship/dispatch - self-ship dispatch', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/orders/shipments/self-ship/dispatch', {
      ShipmentId: 'SH001',
      TentativeDeliveryDate: '2026-07-12',
      DispatchDate: '2026-07-09',
      DeliveryPartner: 'DTDC',
      DeliveryPartnerCode: 'DTDC001',
      TrackingId: 'TRACK12345',
      LocationId: 'loc-001',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/orders/shipments/self-ship/delivery - self-ship delivery', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/orders/shipments/self-ship/delivery', {
      ShipmentId: 'SH001',
      DeliveryDate: '2026-07-12',
      LocationId: 'loc-001',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/flipkart/orders/shipments/otc/{locationId} - OTC details', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/orders/shipments/otc/loc-001', { requestType: 'PICKUP' });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/orders/services/attempt - service attempt', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/orders/services/attempt', {
      Services: [
        {
          ServiceFulfilmentId: 'SVC001',
          DeliveryDate: '2026-07-09',
          NewDeliveryDate: '2026-07-11',
          Reason: 'CUSTOMER_NOT_AVAILABLE',
          Comments: 'Will retry tomorrow',
        },
      ],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/orders/services/complete - service complete', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/orders/services/complete', {
      Services: [
        {
          ServiceFulfilmentId: 'SVC001',
          DeliveryDate: '2026-07-09',
        },
      ],
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 4. Returns Management ─────────────────────────────────────────────────────

test.describe('Flipkart - Returns APIs', () => {
  test('GET /api/flipkart/returns - get all returns', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/returns', {
      source: 'customer_return',
      createdAfter: '2026-06-01T00:00:00Z',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/flipkart/returns?returnIds= - get returns by ID', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/returns', { returnIds: 'RET001,RET002' });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/returns/approve - approve returns', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/returns/approve', {
      Returns: [
        {
          ReturnId: 'RET001',
          Comments: 'Approved',
          ServiceDate: '2026-07-10',
          ServiceFulfilmentId: 'SVC001',
          LocationId: 'loc-001',
        },
      ],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/returns/reject - reject returns', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/returns/reject', {
      Returns: [
        {
          ReturnId: 'RET002',
          Comments: 'Item not defective',
          LocationId: 'loc-001',
        },
      ],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/returns/complete - complete returns', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/returns/complete', {
      LocationId: 'loc-001',
      ReturnIds: 'RET001',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/returns/pickup - schedule pickup', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/returns/pickup', {
      Returns: [
        {
          ReturnId: 'RET001',
          PickupDate: '2026-07-10',
          LocationId: 'loc-001',
        },
      ],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/returns/pickup-attempt - record pickup attempt', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/returns/pickup-attempt', {
      Returns: [
        {
          ReturnId: 'RET001',
          PickupDate: '2026-07-10',
          Reason: 'SELLER_NOT_AVAILABLE',
          NewPickupDate: '2026-07-11',
          LocationId: 'loc-001',
        },
      ],
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/returns/tasks/initial-load - initial return load', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/returns/tasks/initial-load', {
      SellerLocationId: 'loc-001',
      TaskType: 'RETURN',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/returns/otc/start - start OTC return', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/returns/otc/start', {
      SellerLocationId: 'loc-001',
      TaskType: 'RETURN',
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/returns/tasks/shipments - get return task shipments', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/returns/tasks/shipments', {
      TaskId: 'TASK001',
      FeId: 'FE001',
      PageSize: 25,
    });
    expect([200, 400]).toContain(res.status());
  });

  test('POST /api/flipkart/returns/otc/end - end OTC return', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/returns/otc/end', {
      TaskId: 'TASK001',
      FeId: 'FE001',
      TaskType: 'RETURN',
    });
    expect([200, 400]).toContain(res.status());
  });
});

// ── 5. Report Management ─────────────────────────────────────────────────────

test.describe('Flipkart - Report APIs', () => {
  test('POST /api/flipkart/reports - request report', async ({ request }) => {
    const res = await fpost(request, '/api/flipkart/reports', {
      ReportTypeIdentifier: 'SELLER_RETURNS_REPORT',
      CorrelationId: '550e8400-e29b-41d4-a716-446655440000',
      Parameters: { from_date: '2026-06-01', to_date: '2026-06-30' },
    });
    expect([200, 400]).toContain(res.status());
  });

  test('GET /api/flipkart/reports/{reportId} - get report detail', async ({ request }) => {
    const res = await fget(request, '/api/flipkart/reports/RPT-12345');
    expect([200, 400]).toContain(res.status());
  });
});
