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
    const [users, setUsers] = useAtom(UsersAtom);
    const [services, setServices] = useAtom(ServicesAtom);
    const [activityLogs, setActivityLogs] = useAtom(ActivityLogsAtom);
    const [latestActivityLogs, setLatestActivityLogs] = useAtom(LatestActivityLogsAtom);
    const [bookings, setBookings] = useAtom(BookingsAtom);
    const [latestBookings, setLatestBookings] = useAtom(LatestBookingsAtom);
    const [topicAtom, setTopicAtom] = useAtom(TopicAtom);
    const [jwt] = useAtom(JwtAtom);

    useEffect(() => {
        if (!services || services.length === 0) {
            serviceClient.getAllServices().then(setServices);
        }
    }, [services, setServices]);

    useEffect(() => {
        if (!users || users.length === 0) {
            userClient.getAllUsers(jwt).then(setUsers);
        }
    }, [users, jwt, setUsers]);

    useEffect(() => {
        if (!activityLogs || activityLogs.length === 0) {
            activityLogsClient.getAllActivityLogs().then(setActivityLogs);
        }
    }, [activityLogs, setActivityLogs]);

    useEffect(() => {
        if (!latestActivityLogs || latestActivityLogs.length === 0) {
            activityLogsClient.getLatestActivityLogs().then(setLatestActivityLogs);
        }
    }, [latestActivityLogs, setLatestActivityLogs]);

    useEffect(() => {
        if (!bookings || bookings.length === 0) {
            bookingClient.getAllBookings(jwt).then(setBookings);
        }
    }, [bookings, jwt, setBookings]);

    useEffect(() => {
        if (!latestBookings || latestBookings.length === 0) {
            bookingClient.getLatestBookings(jwt).then(setLatestBookings);
        }
    }, [latestBookings, jwt, setLatestBookings]);

    useEffect(() => {
        if (!topicAtom || topicAtom.length === 0) {
            subscriptionClient.subscribeToDashboard(jwt, randomUid).then(() => {
                setTopicAtom("dashboard");
                toast.success("Successfully joined dashboard topic");
            });
        }
    }, [jwt]);
}
