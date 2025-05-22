import {useAtom} from "jotai";
import {ActivityLogsAtom} from "../../atoms/atoms.ts";

const ActivityLogs = () => {

    const [activityLogs] = useAtom(ActivityLogsAtom);

    return (

        <div className="overflow-x-auto rounded-box border border-base-content/5 mt-4">
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
                {activityLogs.map((log) => (
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