import {useAtom} from "jotai";
import {ActivityLogsAtom, JwtAtom, LatestActivityLogsAtom, ServicesAtom, UsersAtom,} from "../atoms/atoms.ts";
import {useEffect} from "react";
import {activityLogsClient, serviceClient, userClient} from "../apiControllerClients.ts";

export default function useInitializeData() {
    const [, setUsers] = useAtom(UsersAtom);
    const [, setServices] = useAtom(ServicesAtom);
    const [, setActivityLogs] = useAtom(ActivityLogsAtom);
    const [, setLatestActivityLogs] = useAtom(LatestActivityLogsAtom);
    const [jwt] = useAtom(JwtAtom);

    useEffect(() => {
        serviceClient.getAllServices().then(r => {
            setServices(r);
        })
    }, [setServices])

    useEffect(() => {
        userClient.getAllUsers(jwt).then(r => {
            setUsers(r);
        })
    }, [setUsers])

    useEffect(() => {
        activityLogsClient.getActivityLogs().then(r => {
            setActivityLogs(r);
        })
    }, [setActivityLogs])

    useEffect(() => {
        activityLogsClient.getLatestActivityLogs().then(r => {
            setLatestActivityLogs(r);
        })
    }, [setLatestActivityLogs])
}