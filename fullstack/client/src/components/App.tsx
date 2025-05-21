import {WsClientProvider} from 'ws-request-hook';
import {useEffect, useState} from "react";
import Home from "./Home.tsx";
import {Route, Routes} from "react-router";
import {
    AccountActivationRoute,
    ForgotPasswordRoute,
    ResetPasswordRoute,
    SignInRoute
} from "../helpers/routeConstants.tsx";
import SignIn from "./SignIn.tsx";
import AccountActivation from "./AccountActivation/AccountActivation.tsx";
import ForgotPassword from "./ForgotPassword/ForgotPassword.tsx";
import ResetPassword from "./ResetPassword/ResetPassword.tsx";
const baseUrl = import.meta.env.VITE_API_BASE_URL
const prod = import.meta.env

export const randomUid = crypto.randomUUID()

export default function App() {
    
    const [url, setUrl] = useState<string | undefined>(undefined)
    useEffect(() => {
        const finalUrl = prod ? 'wss://' + baseUrl + '?id=' + randomUid : 'ws://' + baseUrl + '?id=' + randomUid;
setUrl(finalUrl);
    }, [prod, baseUrl]);
    
    return (<>

        {
            url &&
        <WsClientProvider url={url}>

            <div className="flex flex-col">
                <div>
                    <Routes>
                        {/* public routes */}
                        <Route path={SignInRoute} element={<SignIn />} />
                        <Route path={AccountActivationRoute} element={<AccountActivation />} />
                        <Route path={ResetPasswordRoute} element={<ResetPassword />} />
                        <Route path={ForgotPasswordRoute} element={<ForgotPassword />} />

                        {/* private routes */}
                        <Route path="/*" element={<Home/>}/>
                    </Routes>
                </div>

            </div>
        </WsClientProvider>
        }
    </>)
}