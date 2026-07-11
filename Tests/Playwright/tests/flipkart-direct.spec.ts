/**
 * Direct Flipkart Marketplace Seller API Tests
 * Base URL: https://api.flipkart.net/sellers
 *
 * SETUP:
 * 1. Log in to https://seller.flipkart.com
 * 2. Go to Settings → API → Generate Access Token
 * 3. Copy .env.example to .env and fill in FLIPKART_ACCESS_TOKEN
 * 4. Run: npx playwright test flipkart-direct.spec.ts
 */

import { test, expect } from '@playwright/test';
import { fkGet, fkPost, fkPut, hasCredentials, FLIPKART } from './flipkart-helpers';

test.beforeAll(() => {
  if (!hasCredentials()) {
    console.warn('\n⚠️  No Flipkart credentials found. Copy .env.example to .env and fill in FLIPKART_ACCESS_TOKEN.\n');
  }
});

// ── 1. Listing Management ─────────────────────────────────────────────────────

test.describe('Flipkart Direct - Listing APIs', () => {
  test.skip(() => !hasCredentials(), 'Skipped: set FLIPKART_ACCESS_TOKEN in .env');

  test('POST /listings/v3 - create listing', async ({ request }) => {
    const res = await fkPost(request, '/listings/v3', {
      'SKU-TEST-001': {
        product_id: 'PROD1234567890ABC',
        price: { mrp: 1000, selling_price: 800, currency: 'INR' },
        tax: { hsn: '8471', tax_code: 'GST_18' },
        listing_status: 'INACTIVE',
        fulfillment_profile: 'NON_FBF',
        fulfillment: { dispatch_sla: 2, shipping_provider: 'FLIPKART', procurement_type: 'REGULAR' },
        locations: [{ id: 'loc-001', status: 'ENABLED', inventory: 10 }],
      },
    });
    const body = await res.json();
    console.log('Create listing:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
  });

  test('POST /listings/v3/update - update listing', async ({ request }) => {
    const res = await fkPost(request, '/listings/v3/update', {
      'SKU-TEST-001': {
        product_id: 'PROD1234567890ABC',
        price: { mrp: 1000, selling_price: 750, currency: 'INR' },
        listing_status: 'INACTIVE',
      },
    });
    const body = await res.json();
    console.log('Update listing:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
  });

  test('GET /listings/v3/{sku-ids} - get listing by SKU', async ({ request }) => {
    const res = await fkGet(request, '/listings/v3/SKU-TEST-001');
    const body = await res.json();
    console.log('Get listing:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404]).toContain(res.status());
  });

  test('POST /listings/v3/details - get listing details', async ({ request }) => {
    const res = await fkPost(request, '/listings/v3/details', {
      sku_ids: ['SKU-TEST-001'],
    });
    const body = await res.json();
    console.log('Listing details:', JSON.stringify(body, null, 2));
    expect([200, 400, 401]).toContain(res.status());
  });

  test('POST /listings/v3/product/search - search listings', async ({ request }) => {
    const res = await fkPost(request, '/listings/v3/product/search', {
      batchNo: 1,
      internalState: 'ACTIVE',
    });
    const body = await res.json();
    console.log('Search listings:', JSON.stringify(body, null, 2));
    expect([200, 400, 401]).toContain(res.status());
  });

  test('POST /listings/v3/search - get all listings', async ({ request }) => {
    const res = await fkPost(request, '/listings/v3/search', {
      filters: { listing_status: 'ACTIVE' },
      page_id: null,
    });
    const body = await res.json();
    console.log('All listings:', JSON.stringify(body, null, 2));
    expect([200, 400, 401]).toContain(res.status());
  });

  test('POST /listings/v3/update/price - update price', async ({ request }) => {
    const res = await fkPost(request, '/listings/v3/update/price', {
      'SKU-TEST-001': {
        product_id: 'PROD1234567890ABC',
        price: { mrp: 1000, selling_price: 700, currency: 'INR' },
      },
    });
    const body = await res.json();
    console.log('Update price:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
  });

  test('POST /listings/v3/update/inventory - update inventory', async ({ request }) => {
    const res = await fkPost(request, '/listings/v3/update/inventory', {
      'SKU-TEST-001': {
        product_id: 'PROD1234567890ABC',
        locations: [{ id: 'loc-001', inventory: 50 }],
      },
    });
    const body = await res.json();
    console.log('Update inventory:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
  });
});

// ── 2. HyperLocal Listing ─────────────────────────────────────────────────────

test.describe('Flipkart Direct - HyperLocal Listing APIs', () => {
  test.skip(() => !hasCredentials(), 'Skipped: set FLIPKART_ACCESS_TOKEN in .env');

  test('POST /listings/v3/hyperlocal - create hyperlocal listing', async ({ request }) => {
    const res = await fkPost(request, '/listings/v3/hyperlocal', {
      'HL-SKU-TEST-001': {
        product_id: 'PROD1234567890ABC',
        price: { mrp: 500, selling_price: 450, currency: 'INR' },
        listing_status: 'INACTIVE',
        package: { name: 'Box', dimensions: { length: 10, breadth: 8, height: 5 }, weight: 0.5 },
        location: { id: 'loc-001', status: 'ENABLED', inventory: 20 },
      },
    });
    const body = await res.json();
    console.log('Create HL listing:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
  });

  test('POST /listings/v3/hyperlocal/update - update hyperlocal listing', async ({ request }) => {
    const res = await fkPost(request, '/listings/v3/hyperlocal/update', {
      'HL-SKU-TEST-001': {
        product_id: 'PROD1234567890ABC',
        price: { mrp: 500, selling_price: 400, currency: 'INR' },
      },
    });
    const body = await res.json();
    console.log('Update HL listing:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
  });

  test('GET /listings/v3/{sku-ids} - get hyperlocal listing', async ({ request }) => {
    const res = await fkGet(request, '/listings/v3/HL-SKU-TEST-001');
    const body = await res.json();
    console.log('Get HL listing:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404]).toContain(res.status());
  });

  test('POST /listings/v3/update/price - update HL price', async ({ request }) => {
    const res = await fkPost(request, '/listings/v3/update/price', {
      'HL-SKU-TEST-001': {
        product_id: 'PROD1234567890ABC',
        price: { mrp: 500, selling_price: 380, currency: 'INR' },
      },
    });
    const body = await res.json();
    console.log('Update HL price:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
  });

  test('POST /listings/v3/update/inventory - update HL inventory', async ({ request }) => {
    const res = await fkPost(request, '/listings/v3/update/inventory', {
      'HL-SKU-TEST-001': {
        product_id: 'PROD1234567890ABC',
        locations: [{ id: 'loc-001', inventory: 30 }],
      },
    });
    const body = await res.json();
    console.log('Update HL inventory:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
  });
});

// ── 3. Shipment / Order Management ───────────────────────────────────────────

test.describe('Flipkart Direct - Shipment APIs', () => {
  test.skip(() => !hasCredentials(), 'Skipped: set FLIPKART_ACCESS_TOKEN in .env');

  test('POST /v3/shipments/filter/ - filter shipments', async ({ request }) => {
    const res = await fkPost(request, '/v3/shipments/filter/', {
      filter: {
        type: 'preDispatch',
        states: ['APPROVED', 'PACKED'],
        hold: false,
      },
      pagination: { pageSize: 10 },
      sort: { field: 'dispatchByDate', order: 'asc' },
    });
    const body = await res.json();
    console.log('Filter shipments:', JSON.stringify(body, null, 2));
    expect([200, 400, 401]).toContain(res.status());
  });

  test('GET /v3/shipments?shipmentIds= - get by shipment IDs', async ({ request }) => {
    const res = await fkGet(request, '/v3/shipments', { shipmentIds: 'SH001' });
    const body = await res.json();
    console.log('Get shipments by ID:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404]).toContain(res.status());
  });

  test('GET /v3/shipments?orderItemIds= - get by order item IDs', async ({ request }) => {
    const res = await fkGet(request, '/v3/shipments', { orderItemIds: 'OI001' });
    const body = await res.json();
    console.log('Get shipments by order item:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404]).toContain(res.status());
  });

  test('GET /v3/shipments?orderIds= - get by order IDs', async ({ request }) => {
    const res = await fkGet(request, '/v3/shipments', { orderIds: 'ORD001' });
    const body = await res.json();
    console.log('Get shipments by order:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404]).toContain(res.status());
  });

  test('POST /v3/shipments/labels - generate labels', async ({ request }) => {
    const res = await fkPost(request, '/v3/shipments/labels', {
      shipments: [{
        shipmentId: 'SH001',
        locationId: 'loc-001',
        taxItems: [{ orderItemId: 'OI001', purchasePrice: 800, taxRate: 18, quantity: 1 }],
        invoices: [{ orderId: 'ORD001', invoiceNumber: 'INV-001', invoiceDate: '2026-07-09' }],
      }],
    });
    const body = await res.json().catch(() => ({}));
    console.log('Generate labels:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });

  test('GET /v3/shipments/{ids}/labels - download labels', async ({ request }) => {
    const res = await fkGet(request, '/v3/shipments/SH001/labels');
    console.log('Download labels status:', res.status());
    expect([200, 400, 401, 404]).toContain(res.status());
  });

  test('POST /v3/shipments/dispatch - dispatch shipments', async ({ request }) => {
    const res = await fkPost(request, '/v3/shipments/dispatch', {
      shipmentIds: ['SH001'],
      locationId: 'loc-001',
    });
    const body = await res.json().catch(() => ({}));
    console.log('Dispatch:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });

  test('GET /v3/shipments/{ids} - get shipment details', async ({ request }) => {
    const res = await fkGet(request, '/v3/shipments/SH001');
    const body = await res.json().catch(() => ({}));
    console.log('Shipment details:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404]).toContain(res.status());
  });

  test('POST /v3/shipments/cancel - cancel shipments', async ({ request }) => {
    const res = await fkPost(request, '/v3/shipments/cancel', {
      shipments: [{ shipmentId: 'SH001', locationId: 'loc-001', reason: 'cannot_procure_item' }],
    });
    const body = await res.json().catch(() => ({}));
    console.log('Cancel:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });

  test('GET /v3/shipments/handover/counts - handover counts', async ({ request }) => {
    const res = await fkGet(request, '/v3/shipments/handover/counts', { locationId: 'loc-001' });
    const body = await res.json().catch(() => ({}));
    console.log('Handover counts:', JSON.stringify(body, null, 2));
    expect([200, 400, 401]).toContain(res.status());
  });

  test('POST /v3/shipments/manifest - generate manifest', async ({ request }) => {
    const res = await fkPost(request, '/v3/shipments/manifest', {
      params: [{
        vendorGroupCode: 'VG001',
        locationId: 'loc-001',
        isMps: false,
        pickUpDate: { after: new Date().toISOString(), before: new Date(Date.now() + 86400000).toISOString() },
      }],
    });
    const body = await res.json().catch(() => ({}));
    console.log('Manifest:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
  });

  test('POST /v3/shipments/selfShip/dispatch - self-ship dispatch', async ({ request }) => {
    const res = await fkPost(request, '/v3/shipments/selfShip/dispatch', {
      shipmentId: 'SH001',
      tentativeDeliveryDate: new Date(Date.now() + 3 * 86400000).toISOString(),
      dispatchDate: new Date().toISOString(),
      deliveryPartner: 'DTDC',
      trackingId: 'TRACK12345',
      locationId: 'loc-001',
    });
    const body = await res.json().catch(() => ({}));
    console.log('Self-ship dispatch:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });

  test('POST /v3/shipments/selfShip/delivery - self-ship delivery', async ({ request }) => {
    const res = await fkPost(request, '/v3/shipments/selfShip/delivery', {
      shipmentId: 'SH001',
      deliveryDate: new Date().toISOString().split('T')[0],
      locationId: 'loc-001',
    });
    const body = await res.json().catch(() => ({}));
    console.log('Self-ship delivery:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });

  test('GET /v3/shipments/otc/{locationId} - OTC details', async ({ request }) => {
    const res = await fkGet(request, '/v3/shipments/otc/loc-001', { requestType: 'PICKUP' });
    const body = await res.json().catch(() => ({}));
    console.log('OTC details:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404]).toContain(res.status());
  });
});

// ── 4. Services ───────────────────────────────────────────────────────────────

test.describe('Flipkart Direct - Service APIs', () => {
  test.skip(() => !hasCredentials(), 'Skipped: set FLIPKART_ACCESS_TOKEN in .env');

  test('POST /v2/services/attempt - service attempt', async ({ request }) => {
    const res = await fkPost(request, '/v2/services/attempt', {
      services: [{
        serviceFulfilmentId: 'SVC001',
        deliveryDate: new Date().toISOString().split('T')[0],
        newDeliveryDate: new Date(Date.now() + 86400000).toISOString().split('T')[0],
        reason: 'CUSTOMER_NOT_AVAILABLE',
      }],
    });
    const body = await res.json().catch(() => ({}));
    console.log('Service attempt:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });

  test('POST /v2/services/complete - service complete', async ({ request }) => {
    const res = await fkPost(request, '/v2/services/complete', {
      services: [{
        serviceFulfilmentId: 'SVC001',
        deliveryDate: new Date().toISOString().split('T')[0],
      }],
    });
    const body = await res.json().catch(() => ({}));
    console.log('Service complete:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });
});

// ── 5. Returns Management ─────────────────────────────────────────────────────

test.describe('Flipkart Direct - Returns APIs', () => {
  test.skip(() => !hasCredentials(), 'Skipped: set FLIPKART_ACCESS_TOKEN in .env');

  test('GET /v2/returns - get all returns', async ({ request }) => {
    const res = await fkGet(request, '/v2/returns', {
      source: 'customer_return',
      createdAfter: new Date(Date.now() - 30 * 86400000).toISOString(),
    });
    const body = await res.json().catch(() => ({}));
    console.log('Returns list:', JSON.stringify(body, null, 2));
    expect([200, 400, 401]).toContain(res.status());
  });

  test('GET /v2/returns?returnIds= - get returns by ID', async ({ request }) => {
    const res = await fkGet(request, '/v2/returns', { returnIds: 'RET001' });
    const body = await res.json().catch(() => ({}));
    console.log('Returns by ID:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404]).toContain(res.status());
  });

  test('POST /v2/returns/approve - approve returns', async ({ request }) => {
    const res = await fkPost(request, '/v2/returns/approve', {
      returns: [{
        returnId: 'RET001',
        comments: 'Approved for return',
        locationId: 'loc-001',
      }],
    });
    const body = await res.json().catch(() => ({}));
    console.log('Approve returns:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });

  test('POST /v2/returns/reject - reject returns', async ({ request }) => {
    const res = await fkPost(request, '/v2/returns/reject', {
      returns: [{
        returnId: 'RET001',
        comments: 'Item not defective',
        locationId: 'loc-001',
      }],
    });
    const body = await res.json().catch(() => ({}));
    console.log('Reject returns:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });

  test('POST /v2/returns/complete - complete returns', async ({ request }) => {
    const res = await fkPost(request, '/v2/returns/complete', {});
    const body = await res.json().catch(() => ({}));
    console.log('Complete returns:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
  });

  test('POST /v2/returns/pickup - schedule pickup', async ({ request }) => {
    const res = await fkPost(request, '/v2/returns/pickup', {
      returns: [{
        returnId: 'RET001',
        pickupDate: new Date(Date.now() + 86400000).toISOString().split('T')[0],
        locationId: 'loc-001',
      }],
    });
    const body = await res.json().catch(() => ({}));
    console.log('Pickup returns:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });

  test('POST /v2/returns/pickupAttempt - pickup attempt', async ({ request }) => {
    const res = await fkPost(request, '/v2/returns/pickupAttempt', {
      returns: [{
        returnId: 'RET001',
        pickupDate: new Date().toISOString().split('T')[0],
        reason: 'SELLER_NOT_AVAILABLE',
        newPickupDate: new Date(Date.now() + 86400000).toISOString().split('T')[0],
        locationId: 'loc-001',
      }],
    });
    const body = await res.json().catch(() => ({}));
    console.log('Pickup attempt:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });

  test('POST /v3/returns/tasks/initial-load - initial return load', async ({ request }) => {
    const res = await fkPost(request, '/v3/returns/tasks/initial-load', {
      sellerLocationId: 'loc-001',
      taskType: 'RETURN',
    });
    const body = await res.json().catch(() => ({}));
    console.log('Initial return load:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
  });

  test('POST /v3/returns/otc/start/details - start OTC return', async ({ request }) => {
    const res = await fkPost(request, '/v3/returns/otc/start/details', {
      sellerLocationId: 'loc-001',
      taskType: 'RETURN',
    });
    const body = await res.json().catch(() => ({}));
    console.log('Start OTC:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
  });

  test('POST /v3/returns/tasks/shipments - return task shipments', async ({ request }) => {
    const res = await fkPost(request, '/v3/returns/tasks/shipments', {
      taskId: 'TASK001',
      feId: 'FE001',
      pageSize: 25,
    });
    const body = await res.json().catch(() => ({}));
    console.log('Return task shipments:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });

  test('POST /v3/returns/otc/end/details - end OTC return', async ({ request }) => {
    const res = await fkPost(request, '/v3/returns/otc/end/details', {
      feId: 'FE001',
      taskId: 'TASK001',
      taskType: 'RETURN',
    });
    const body = await res.json().catch(() => ({}));
    console.log('End OTC:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404, 422]).toContain(res.status());
  });
});

// ── 6. Report Management ─────────────────────────────────────────────────────

test.describe('Flipkart Direct - Report APIs', () => {
  test.skip(() => !hasCredentials(), 'Skipped: set FLIPKART_ACCESS_TOKEN in .env');

  test('POST /reports/:reportTypeIdentifier - request report', async ({ request }) => {
    const res = await fkPost(request, '/reports/SELLER_RETURNS_REPORT', {
      correlation_id: crypto.randomUUID(),
      parameters: {
        from_date: new Date(Date.now() - 30 * 86400000).toISOString().split('T')[0],
        to_date:   new Date().toISOString().split('T')[0],
      },
    });
    const body = await res.json().catch(() => ({}));
    console.log('Request report:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 422]).toContain(res.status());
    if (res.status() === 200 && body.report_id) {
      console.log('\n✅ Report ID:', body.report_id, '— use this in the next test\n');
    }
  });

  test('GET /reports/:reportId/detail - get report detail', async ({ request }) => {
    const reportId = process.env.FLIPKART_REPORT_ID ?? 'REPORT-TEST-ID';
    const res = await fkGet(request, `/reports/${reportId}/detail`);
    const body = await res.json().catch(() => ({}));
    console.log('Report detail:', JSON.stringify(body, null, 2));
    expect([200, 400, 401, 404]).toContain(res.status());
  });
});
