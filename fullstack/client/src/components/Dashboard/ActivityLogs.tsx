import {useWsClient} from "ws-request-hook";
import {useEffect} from "react";
import {useAtom} from "jotai";
import {LatestActivityLogsAtom} from "../../atoms/atoms.ts";
import toast from "react-hot-toast";

const ActivityLogs = () => {

    interface ActivityLog {
        eventType: string,
        requestId: string,
        activityLogs: []
    }

    const [latestActivityLogs, setLatestActivityLogs] = useAtom(LatestActivityLogsAtom);
    const {onMessage, readyState} = useWsClient();


    useEffect(() => {
        if (readyState != 1) return;
        try {
            const unsubscribe = onMessage<ActivityLog>("ActivityLogsBroadcastDto", (dto) => {
                setLatestActivityLogs(dto.activityLogs);
                toast.success("Activity logs updated");
            });
            return () => {
                unsubscribe();
            };
        } catch (e) {
            console.error("Error in AllLogs: ", e);
            toast.error("Error in AllLogs: " + e);
        }
    });

    return (
        <div className="overflow-x-auto rounded-box border border-base-content/5  mt-4">
            <table className="table">
                <thead>
                    <tr>
                        <th>Service name</th>
                        <th>Full name</th>
                        <th>Time of attempt</th>
                        <th>Date of attempt</th>
                        <th>Status</th>
                    </tr>
            </thead>
            <tbody>
            {latestActivityLogs.map((log) => (
                <tr key={log.id}>
                    <td>{log.serviceName}</td>
                    <td>{log.fullname}</td>
                    <td>{log.time}</td>
                    <td>{log.date}</td>
                    <td>{log.status}</td>
                </tr>
            ))}

            </tbody>
        </table>
    </div>
    )
}


export default ActivityLogs;