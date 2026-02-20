# 📝 DOTNET MAUI MULTI-PLATFORM PROJECT
Dotnet maui project with a docker container for web deployment

---
# ⚙️ STRUCTURE
The project contains the following structure for the native deployment (Android, IOS, Windows) + Blazor web application.

```
/DOCKERMAUI (Root)
│
├── /SharedUI           # (Razor Class Library)
├── /AppMaui            # (Projeto Nativo - Android/Windows/iOS)
├── /WebApp             # (Projeto Blazor Server/Wasm - O que vai pro Docker)
│
└── Dockerfile          # (Config)

```
---
# 🛜 Connecting to OpenAPI

This project uses OpenAPI's Identity system to manage user login with hashed passwords and 2FA.
Certain endpoints may require an authentication token, which is obtained after registering and logging.

1. Register with the 'register' endpoint:

	```
 		curl -X 'POST' \
		'http://localhost:5020/register' \
		-H 'accept: */*' \
		-H 'Authorization: Bearer CfDJ8O4hL85mJ45Hv7hRgAZsulHmcsGdI1-Wz_wB8si9G5XLp_V5CrjIraZg547RjsDb-kEvjqoKsW2rDzWdH9gdAjShXbkUi2HEwX2PWUjaGZ_RhtpqY6L3wzbn_7fe_fDtH_Fsw9wfsCrBWOPSBtSkxcNL_N2EhNKSie_0RLlUsPtxDWW-			L0un9U2KUsE5mECie--JJByVR3ltE2wvX2NIeOz_OnPBEZ5Slc4VpoVKZnshcdNJI0U03IMYgK3M15Sym2AnMeVxDhhWl0zfeY0e0SZhOVtv2x6xlW2MflLMxCbLd5RIZhY53Dr0q4vKOVzWrjY9z_3fFCDNziHej10JsVit5oUu6KvtPEATksub0sDGfgaYuj_qo8VFQc0_rc-			yDqKJvefDTXR1JgL180YVdrhls4rXUTJZqA8_AoFrEEnjR2wY-wfUB37YH9TDL1aC7uhZQ4jEdcH3fuVxUPwoFM_7SkA2PT0sox_KHDHC1lWx17n32jqenZn9j9XHi0XQd2gAlwuXWSj1_61DeolCc3zytWF-								qvKDFgEqLwgnXscX72tslDs6Q6UUK6S14BJN2XOZ8wfoQ1c6lHAlY2Ek-4qJJ5i4FKaKICZZIVGxFAR8MXSH_T6tFzXtny02iDDDMZpMatwohqngtMRxkpkQ68ac3dxi22ReN7YFkEtkJl0sXWe-1W0O9nlRhuX52fqDg8gJlSvZ5HQzuawjodmiNgY' \
		-H 'Content-Type: application/json' \
		-d '{
		"email": "Test12345@gmail.com",
		"password": "test123TEST@"'
	```

3. Attempt login with the 'login' endpoint. The api will return a JWT response:
   
   
			curl -X 'POST' \
			'http://localhost:5020/login?useCookies=false&useSessionCookies=false' \
			-H 'accept: application/json' \
			-H 'Content-Type: application/json' \
			-d '{
			"email": "Test12345@gmail.com",
			"password": "test123TEST@""
			}'
	response:

		{
		"tokenType": "Bearer",
		"accessToken": "CfDJ8O4hL85mJ45Hv7hRgAZsulHmcsGdI1-Wz_wB8si9G5XLp_V5CrjIraZg547RjsDb-kEvjqoKsW2rDzWdH9gdAjShXbkUi2HEwX2PWUjaGZ_RhtpqY6L3wzbn_7fe_fDtH_Fsw9wfsCrBWOPSBtSkxcNL_N2EhNKSie_0RLlUsPtxDWW-				L0un9U2KUsE5mECie--JJByVR3ltE2wvX2NIeOz_OnPBEZ5Slc4VpoVKZnshcdNJI0U03IMYgK3M15Sym2AnMeVxDhhWl0zfeY0e0SZhOVtv2x6xlW2MflLMxCbLd5RIZhY53Dr0q4vKOVzWrjY9z_3fFCDNziHej10JsVit5oUu6KvtPEATksub0sDGfgaYuj_qo8VFQc0_rc-yDqKJvefDTXR1JgL180YVdrhls4rXUTJZqA8_AoFrEEnjR2wY-wfUB37YH9TDL1aC7uhZQ4jEdcH3fuVxUPwoFM_7SkA2PT0sox_KHDHC1lWx17n32jqenZn9j9XHi0XQd2gAlwuXWSj1_61DeolCc3zytWF-qvKDFgEqLwgnXscX72tslDs6Q6UUK6S14BJN2XOZ8wfoQ1c6lHAlY2Ek-4qJJ5i4FKaKICZZIVGxFAR8MXSH_T6tFzXtny02iDDDMZpMatwohqngtMRxkpkQ68ac3dxi22ReN7YFkEtkJl0sXWe-1W0O9nlRhuX52fqDg8gJlSvZ5HQzuawjodmiNgY",
		"expiresIn": 3600,
		"refreshToken": "CfDJ8O4hL85mJ45Hv7hRgAZsulHcV48ji3Ml5hPhUO7CCv5Nsk1uLp72_MRjR09PrzjG3keznNQK-9fc1frobkEKnj2J8VSmMA1H10CJwCTqN5SxCwqd8_Lg9xMgUiI1qPX-KOlZT40KEqzKivE_v7szBTl6jbYrUij1Li8hVA6FEMUFJCjm1JGq_7rI93vhkeitajeSRuK9zWnyGvZyq5IXn7H8cizmvnTu0gNkyf-YrA9X569r1G6l3KvfGDDH-ccpKsJFnOa0-1m1o6IpuKO9JuM5sqH11nO7YoflRe3PFDCwxzyiuWIMR94pkcgT3dV0tMP8ecQ4aQIFnroXCHfuu2djQJ-PXw1kmhY49C0n8CR9pmFt_c8MdgADXXSlWYS2YRyUlb7CF1vQcszUjx6tVQ-9RyToe3UrgD1mPbfyQS_768V0gFrfmLDk_ZhkNESryG60ws_x4SC6toDQRIIV8bGgaUopoWkKGvXrp6XO-rl2kxY_AEJOTxP82Td5MhgoTPTYzrY5vvva8isWyZ-_1k_mxAhJR6lEIqdp3c8zRgReR-8Zn6yv8C8fcewWbU8H4NdsBuxCXRr-c0sLXltdkf1A6jl17_SG8KQaS9PumFS3-KexFiryf15QVHGqFfDTPW88kAqK5FNdQlqdH-6fBFWMJRIYh6iHp6Ph-JlFCV_6a5GUbJaJdcCdNVlMCB0f8Qtr9G6m3FbTtdZAEdcExkE"
		}
		

5. Request for an endpoint that needs autorization: add the received token alongside the request.

		curl -X 'GET' \
		'http://localhost:5020/manage/info' \
		-H 'accept: application/json' \
		-H 'Authorization: Bearer CfDJ8O4hL85mJ45Hv7hRgAZsulHmcsGdI1-Wz_wB8si9G5XLp_V5CrjIraZg547RjsDb-kEvjqoKsW2rDzWdH9gdAjShXbkUi2HEwX2PWUjaGZ_RhtpqY6L3wzbn_7fe_fDtH_Fsw9wfsCrBWOPSBtSkxcNL_N2EhNKSie_0RLlUsPtxDWW-L0un9U2KUsE5mECie--JJByVR3ltE2wvX2NIeOz_OnPBEZ5Slc4VpoVKZnshcdNJI0U03IMYgK3M15Sym2AnMeVxDhhWl0zfeY0e0SZhOVtv2x6xlW2MflLMxCbLd5RIZhY53Dr0q4vKOVzWrjY9z_3fFCDNziHej10JsVit5oUu6KvtPEATksub0sDGfgaYuj_qo8VFQc0_rc-yDqKJvefDTXR1JgL180YVdrhls4rXUTJZqA8_AoFrEEnjR2wY-wfUB37YH9TDL1aC7uhZQ4jEdcH3fuVxUPwoFM_7SkA2PT0sox_KHDHC1lWx17n32jqenZn9j9XHi0XQd2gAlwuXWSj1_61DeolCc3zytWF-qvKDFgEqLwgnXscX72tslDs6Q6UUK6S14BJN2XOZ8wfoQ1c6lHAlY2Ek-4qJJ5i4FKaKICZZIVGxFAR8MXSH_T6tFzXtny02iDDDMZpMatwohqngtMRxkpkQ68ac3dxi22ReN7YFkEtkJl0sXWe-1W0O9nlRhuX52fqDg8gJlSvZ5HQzuawjodmiNgY'
   
