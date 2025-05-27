import "../../styles.css";
import {FiUserPlus} from "react-icons/fi";
import {useNavigate} from "react-router";
import {RegisterUserRoute} from "../../../helpers/routeConstants.tsx";
import {useAtom} from "jotai";
import {UsersAtom} from "../../../atoms/atoms.ts";

export const TopBar = () => {
    const [users] = useAtom(UsersAtom);
    const navigate = useNavigate();
    return (
        <div className="border-b px-4 mb-4 mt-2 pb-4 border-[--color-text-baby-blue]">
            <div className="flex items-center justify-between p-0.5">
                <div>
                    <span className="text-sm font-bold block">
                    Users
                    </span>
                    <span className="text-xs block">
                    Number of users: {users.length}
                    </span>
                </div>
                <button
                    onClick={() => {navigate(RegisterUserRoute)}}
                    className="flex text-sm items-center gap-2 bg-gray-800 hover:bg-gray-700 hover:text-[--color-text-baby-blue] transition-colors rounded px-3 py-1.5">
                    <FiUserPlus/>
                    <span>Register user</span>
                </button>
            </div>
        </div>
    )
}
