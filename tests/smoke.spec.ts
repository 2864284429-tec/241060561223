import { test, expect } from '@playwright/test';

test.describe('用户端 - 主流程', () => {
  test('首页加载成功', async ({ page }) => {
    await page.goto('/');
    await expect(page).toHaveTitle(/图书馆/);
  });

  test('座位列表页加载成功', async ({ page }) => {
    await page.goto('/Seat/List');
    await expect(page.locator('body')).toBeVisible();
  });

  test('座位详情页 - 有效座位', async ({ page }) => {
    await page.goto('/Seat/Detail/1');
    await expect(page.locator('body')).toBeVisible();
  });

  test('座位详情页 - 不存在的座位返回404', async ({ request }) => {
    const resp = await request.get('/Seat/Detail/999');
    expect(resp.status()).toBe(404);
  });
});

test.describe('管理端 - 路由与权限', () => {
  test('登录页加载成功', async ({ page }) => {
    await page.goto('/Admin/Login');
    await expect(page.locator('body')).toBeVisible();
  });

  test('预约列表 - 未登录应重定向到登录页', async ({ page }) => {
    await page.goto('/Admin/ReservationList');
    await page.waitForURL('**/Admin/Login');
    expect(page.url()).toContain('/Admin/Login');
  });

  test('座位管理 - 未登录应重定向到登录页', async ({ page }) => {
    await page.goto('/Admin/SeatList');
    await page.waitForURL('**/Admin/Login');
    expect(page.url()).toContain('/Admin/Login');
  });
});

test.describe('管理端 - 登录流程', () => {
  test('登录成功后跳转到预约列表', async ({ page }) => {
    await page.goto('/Admin/Login');
    await page.fill('input[name="username"]', 'admin');
    await page.fill('input[name="password"]', '123456');
    await page.click('button[type="submit"]');
    await page.waitForURL('**/Admin/ReservationList');
    expect(page.url()).toContain('/Admin/ReservationList');
  });

  test('登录成功后可访问座位管理页', async ({ page }) => {
    await page.goto('/Admin/Login');
    await page.fill('input[name="username"]', 'admin');
    await page.fill('input[name="password"]', '123456');
    await page.click('button[type="submit"]');
    await page.waitForURL('**/Admin/ReservationList');
    await page.goto('/Admin/SeatList');
    await expect(page.locator('body')).toBeVisible();
    expect(page.url()).toContain('/Admin/SeatList');
  });

  test('错误密码显示错误信息', async ({ page }) => {
    await page.goto('/Admin/Login');
    await page.fill('input[name="username"]', 'admin');
    await page.fill('input[name="password"]', 'wrong');
    await page.click('button[type="submit"]');
    await page.waitForLoadState('networkidle');
    await expect(page.locator('body')).toContainText('账号或密码错误');
  });
});
