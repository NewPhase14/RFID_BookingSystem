import { useAtom } from "jotai";
import {
    ActivityLogsAtom,
    BookingsAtom,
    JwtAtom,
    LatestActivityLogsAtom,
    LatestBookingsAtom,
    ServicesAtom, TopicAtom,
    UsersAtom,
} from "../atoms/atoms.ts";
import { useEffect } from "react";
import {
    activityLogsClient,
    bookingClient,
    serviceClient,
    subscriptionClient,
    userClient,
} from "../apiControllerClients.ts";
import { randomUid } from "../components/App.tsx";
import toast from "react-hot-toast";

export default function useInitializeData() {
    const [, setUsers] = useAtom(UsersAtom);
    const [, setServices] = useAtom(ServicesAtom);
    const [, setActivityLogs] = useAtom(ActivityLogsAtom);
    const [, setLatestActivityLogs] = useAtom(LatestActivityLogsAtom);
    const [, setBookings] = useAtom(BookingsAtom);
    const [, setLatestBookings] = useAtom(LatestBookingsAtom);
    const [topicAtom, setTopicAtom] = useAtom(TopicAtom);
    const [jwt] = useAtom(JwtAtom);

    useEffect(() => {
            serviceClient.getAllServices(jwt).then(setServices);
    }, [setServices]);

    useEffect(() => {
            userClient.getAllUsers(jwt).then(setUsers);
    }, [setUsers]);

    useEffect(() => {
            activityLogsClient.getAllActivityLogs(jwt).then(setActivityLogs);
    }, [setActivityLogs]);

    useEffect(() => {
        activityLogsClient.getLatestActivityLogs(jwt).then(setLatestActivityLogs);
    }, [setLatestActivityLogs]);

    useEffect(() => {
            bookingClient.getAllBookings(jwt).then(setBookings);
    }, [setBookings]);

    useEffect(() => {
            bookingClient.getLatestBookings(jwt).then(setLatestBookings);
    }, [setLatestBookings]);

    useEffect(() => {
        if (!topicAtom || topicAtom.length === 0) {
            subscriptionClient.subscribeToDashboard(jwt, randomUid).then(() => {
                setTopicAtom("dashboard");
                toast.success("Successfully joined dashboard topic");
            });
        }
    }, [jwt]);
}
