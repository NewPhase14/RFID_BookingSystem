import {useWsClient} from "ws-request-hook";
import {useEffect} from "react";
import {useAtom} from "jotai";
import {ActivityLogAtom} from "../../atoms/atoms.ts";
import toast from "react-hot-toast";

const ActivityLogs = () => {

    interface ActivityLog {
        eventType: string,
        requestId: string,
        activityLogs: []
    }

    const [activityLogs, setActivityLogs] = useAtom(ActivityLogAtom);
    const {onMessage, readyState} = useWsClient();


    useEffect(() => {
        if (readyState != 1) return;

        try {
            onMessage<ActivityLog>("ActivityLogsResponseDto", (dto) => {
                setActivityLogs(dto.activityLogs);
                toast.success("Activity logs updated");
            });
        }
        catch (e) {
            console.error("Error in ActivityLogs: ", e);
            toast.error("Error in ActivityLogs: " + e);
        }



    }, [readyState]);

    return (

    <div className="overflow-x-auto">
        <table className="table table-xs">
            <thead>
            <tr>
                <th>LogId</th>
                <th>Service name</th>
                <th>Full name</th>
                <th>Time of attempt</th>
                <th>Date of attempt</th>
                <th>Status</th>
            </tr>
            </thead>
            <tbody>
            {activityLogs.map((log) => (
                <tr key={log.id}>
                    <td>{log.id}</td>
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