import Sidebar from "./Sidebar/Sidebar.tsx";
import { Route, Routes, useNavigate } from "react-router";
import Dashboard from "./Dashboard/Dashboard.tsx";
import { ActivityLogs } from "./ActivityLogs/ActivityLogs.tsx";
import { Services } from "./Services/Services.tsx";
import { Users } from "./Users/Users.tsx";
import SignIn from "./SignIn.tsx";
import {
    DashboardRoute,
    SignInRoute,
    LogsRoute,
    ServiceRoute,
    UserRoute,
} from "../helpers/routeConstants.tsx";
import { useAtom } from "jotai";
import { useEffect } from "react";
import { JwtAtom } from "../atoms/atoms.ts";

const Home = () => {
    const navigate = useNavigate();
    const [jwt] = useAtom(JwtAtom);

    useEffect(() => {
        if (!jwt || jwt.length < 1) {
            navigate(SignInRoute);
        }
    }, []);

    if (!jwt || jwt.length < 1) {
        return (
            <div className="flex h-screen items-center justify-center">
                <SignIn/>
            </div>
        );
    }

    return (
        <main className="grid gap-4 p-4 grid-cols-[220px,_1fr]">
            <Sidebar />
            <Routes>
                <Route element={<Dashboard />} path={DashboardRoute} />
                <Route element={<Services />} path={ServiceRoute} />
                <Route element={<Users />} path={UserRoute} />
                <Route element={<ActivityLogs />} path={LogsRoute} />
            </Routes>
        </main>
    );
};

export default Home;
