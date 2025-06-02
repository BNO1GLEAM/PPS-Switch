#include <windows.h>
#include <tchar.h>
#include <string>

#define ID_BTN_ENABLE   101
#define ID_BTN_DISABLE  102
#define ID_BTN_PANEL    103
#define ID_LBL_STATUS   104

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

void SetMouseAcceleration(bool enable) {
	int mouseParams[3];
	SystemParametersInfo(SPI_GETMOUSE, 0, mouseParams, 0);
	mouseParams[2] = enable ? 1 : 0;
	SystemParametersInfo(SPI_SETMOUSE, 0, mouseParams, SPIF_SENDCHANGE);
}

bool GetMouseAcceleration() {
	int mouseParams[3];
	SystemParametersInfo(SPI_GETMOUSE, 0, mouseParams, 0);
	return mouseParams[2] != 0;
}

void OpenMousePanel() {
	ShellExecute(NULL, _T("open"), _T("control.exe"), _T("main.cpl"), NULL, SW_SHOWNORMAL);
}

void UpdateStatus(HWND hStatus) {
	bool enabled = GetMouseAcceleration();
	SetWindowText(hStatus, enabled ? _T("鼠标加速状态：已开启") : _T("鼠标加速状态：已关闭"));
}

int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE, LPSTR, int nCmdShow) {
	WNDCLASS wc = {0};
	wc.lpfnWndProc = WndProc;
	wc.hInstance = hInstance;
	wc.lpszClassName = _T("PPSMouseSwitch");
	wc.hbrBackground = (HBRUSH)(COLOR_BTNFACE+1);
	RegisterClass(&wc);
	
	HWND hwnd = CreateWindow(wc.lpszClassName, _T("PPS鼠标加速开关"),
							 WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX,
							 CW_USEDEFAULT, CW_USEDEFAULT, 340, 200,
							 NULL, NULL, hInstance, NULL);
	
	ShowWindow(hwnd, nCmdShow);
	UpdateWindow(hwnd);
	
	MSG msg;
	while (GetMessage(&msg, NULL, 0, 0)) {
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return (int)msg.wParam;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam) {
	static HWND hStatus, hBtnEnable, hBtnDisable, hBtnPanel;
	switch (msg) {
	case WM_CREATE:
		hStatus = CreateWindow(_T("static"), _T("鼠标加速状态：未知"),
							   WS_CHILD | WS_VISIBLE | SS_CENTER,
							   30, 20, 260, 30, hwnd, (HMENU)ID_LBL_STATUS, NULL, NULL);
		
		hBtnEnable = CreateWindow(_T("button"), _T("打开鼠标加速"),
								  WS_CHILD | WS_VISIBLE,
								  30, 60, 120, 30, hwnd, (HMENU)ID_BTN_ENABLE, NULL, NULL);
		
		hBtnDisable = CreateWindow(_T("button"), _T("关闭鼠标加速"),
								   WS_CHILD | WS_VISIBLE,
								   170, 60, 120, 30, hwnd, (HMENU)ID_BTN_DISABLE, NULL, NULL);
		
		hBtnPanel = CreateWindow(_T("button"), _T("打开鼠标控制面板"),
								 WS_CHILD | WS_VISIBLE,
								 30, 110, 260, 30, hwnd, (HMENU)ID_BTN_PANEL, NULL, NULL);
		
		UpdateStatus(hStatus);
		break;
	case WM_COMMAND:
		switch (LOWORD(wParam)) {
		case ID_BTN_ENABLE:
			SetMouseAcceleration(true);
			MessageBox(hwnd, _T("鼠标加速已打开"), _T("提示"), MB_OK | MB_ICONINFORMATION);
			UpdateStatus(hStatus);
			break;
		case ID_BTN_DISABLE:
			SetMouseAcceleration(false);
			MessageBox(hwnd, _T("鼠标加速已关闭"), _T("提示"), MB_OK | MB_ICONINFORMATION);
			UpdateStatus(hStatus);
			break;
		case ID_BTN_PANEL:
			OpenMousePanel();
			break;
		}
		break;
	case WM_CLOSE:
		DestroyWindow(hwnd);
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hwnd, msg, wParam, lParam);
	}
	return 0;
}
