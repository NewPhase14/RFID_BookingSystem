import "../styles.css";
import {FiCalendar} from "react-icons/fi";
import {useAtom} from "jotai";
import {AdminAtom, JwtAtom} from "../../atoms/atoms.ts";
import {useWsClient} from "ws-request-hook";
import {
    ClientWantsToEnterDashboardDto,
    ServerConfirmsAdditionToDashboard,
    StringConstants
} from "../../models/generated-client.ts";
import toast from "react-hot-toast";
import {useEffect} from "react";


export const TopBar = () => {
    const [jwt] = useAtom(JwtAtom);
    const {sendRequest, onMessage, readyState} = useWsClient();

    const joinDashboard = async () => {



        const joinDashboardDto: ClientWantsToEnterDashboardDto = {
                jwt,
            requestId: crypto.randomUUID(),
                eventType: StringConstants.ClientWantsToEnterDashboardDto,
            };

            try{
                const response = await sendRequest<ClientWantsToEnterDashboardDto, ServerConfirmsAdditionToDashboard>(
                    joinDashboardDto,
                    StringConstants.ServerConfirmsAdditionToDashboard
                );
                toast.success(response.message!);
            }
            catch (e) {
                toast.error("Error joining dashboard");
            }
        };
    useEffect(() => {
        if (readyState!=1) return;

        onMessage(StringConstants.ServerConfirmsAdditionToDashboard, (message: ServerConfirmsAdditionToDashboard) => {
            toast.success(message.message!);
        });
    }, [readyState]);



    return (
        <div className="border-b px-4 mb-4 mt-2 pb-4 border-[--color-text-baby-blue]">
            <div className="flex items-center justify-between p-0.5">
                <div>
                    <span className="text-sm font-bold block">
                    Welcome back, {localStorage.getItem("firstname")}
                    </span>
                    <span className="text-xs block">
                    {Date().split("G")[0]}
                    </span>
                </div>
                <button className="flex text-sm items-center gap-2 bg-gray-800 hover:bg-gray-700 hover:text-[--color-text-baby-blue] transition-colors rounded px-3 py-1.5"
                onClick={joinDashboard}>
                    <FiCalendar/>
                    <span>Prev 6 Months</span>
                </button>
            </div>
        </div>
    );
};

