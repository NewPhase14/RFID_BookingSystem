import {useAtom} from "jotai";
import {
    ActivityLogsAtom,
    BookingsAtom,
    JwtAtom,
    LatestActivityLogsAtom, LatestBookingsAtom,
    ServicesAtom,
    UsersAtom,
} from "../atoms/atoms.ts";
import {useEffect} from "react";
import {
    activityLogsClient,
    bookingClient,
    serviceClient,
    subscriptionClient,
    userClient
} from "../apiControllerClients.ts";
import {randomUid} from "../components/App.tsx";
import toast from "react-hot-toast";

export default function useInitializeData() {
    const [, setUsers] = useAtom(UsersAtom);
    const [, setServices] = useAtom(ServicesAtom);
    const [, setActivityLogs] = useAtom(ActivityLogsAtom);
    const [, setLatestActivityLogs] = useAtom(LatestActivityLogsAtom);
    const [, setBookings] = useAtom(BookingsAtom);
    const [, setLatestBookings] = useAtom(LatestBookingsAtom);
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
        activityLogsClient.getAllActivityLogs().then(r => {
            setActivityLogs(r);
        })
    }, [setActivityLogs])

    useEffect(() => {
        activityLogsClient.getLatestActivityLogs().then(r => {
            setLatestActivityLogs(r);
        })
    }, [setLatestActivityLogs])

    useEffect(() => {
        bookingClient.getAllBookings(jwt).then(r => {
            setBookings(r);
        })
    }, [setBookings])

    useEffect(() => {
        bookingClient.getLatestBookings(jwt).then(r => {
            setLatestBookings(r);
        })
    }, [setLatestBookings])

    useEffect(() => {
        subscriptionClient.subscribeToDashboard(jwt, randomUid).then(r => {
            toast.success("Successfully joined Dashboard topic");
        })
    }, [jwt, randomUid])
}