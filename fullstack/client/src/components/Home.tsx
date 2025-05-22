import Sidebar from "./Sidebar/Sidebar.tsx";
import {Navigate, Route, Routes, useNavigate} from "react-router";
import Dashboard from "./Dashboard/Dashboard.tsx";
import { AllLogs } from "./AllLogs/AllLogs.tsx";
import { Services } from "./Services/OverviewService/Services.tsx";
import { Users } from "./Users/Users.tsx";
import {
    DashboardRoute,
    SignInRoute,
    LogsRoute,
    ServiceRoute,
    UserRoute, CreateServiceRoute, CreateAvailabilityRoute,
} from "../helpers/routeConstants.tsx";
import { useAtom } from "jotai";
import { useEffect } from "react";
import { JwtAtom } from "../atoms/atoms.ts";
import {CreateService} from "./Services/CreateService/CreateService.tsx";
import {CreateAvailability} from "./Services/CreateAvailability/CreateAvailability.tsx";
import "./styles.css"

const Home = () => {
    const navigate = useNavigate();
    const [jwt] = useAtom(JwtAtom);

    useEffect(() => {
        if (!jwt || jwt.length < 1) {
            navigate(SignInRoute);
        }
    }, [jwt]);

    if (!jwt || jwt.length < 1) return null;

    return (
        <main className="grid gap-4 p-4 grid-cols-[220px,_1fr]">
            <Sidebar />
            <Routes>
                <Route element={<Dashboard />} path={DashboardRoute} />
                <Route element={<Services />} path={ServiceRoute} />
                <Route element={<Users />} path={UserRoute} />
                <Route element={<AllLogs />} path={LogsRoute} />
                <Route element={<CreateService/>} path={CreateServiceRoute} />
                <Route element={<CreateAvailability/>} path={CreateAvailabilityRoute}/>
                <Route element={<Navigate to={DashboardRoute}/>} path="*" />
            </Routes>
        </main>
    );
};

export default Home;
