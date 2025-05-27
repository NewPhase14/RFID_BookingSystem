import {AuthRegisterRequestDto} from "../../../models/generated-client.ts";
import {useState} from "react";
import {authClient} from "../../../apiControllerClients.ts";
import {useAtom} from "jotai";
import {JwtAtom, UsersAtom} from "../../../atoms/atoms.ts";
import toast from "react-hot-toast";
import {UserRoute} from "../../../helpers/routeConstants.tsx";
import {useNavigate} from "react-router";

const RegisterView = () => {
    const [email, setEmail] = useState("");
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [rfid, setRfid] = useState("");
    const [role, setRole] = useState("");
    const [users, setUsers] = useAtom(UsersAtom);
    const [jwt] = useAtom(JwtAtom);
    const navigate = useNavigate();


    const registerRequestDto: AuthRegisterRequestDto = {
        email: email,
        firstName: firstName,
        lastName: lastName,
        rfid: rfid,
        role: role
    }

    return (
        <div className="flex items-center justify-center min-h-screen">
            <div className="bg-background-grey border border-white/10 rounded-2xl p-10 max-w-lg w-full">

                <h2 className="text-center text-white text-xl mb-6">Create User</h2>

                <div className="space-y-6">
                    <div>
                        <label className="block mb-2 text-sm text-text-grey">RFID</label>
                        <input
                            type="text"
                            value={rfid}
                            onChange={(e) => setRfid(e.target.value)}
                            required
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        />
                    </div>

                    <div>
                        <label className="block mb-2 text-sm text-text-grey">Email</label>
                        <input
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        />
                    </div>

                    <div>
                        <label className="block mb-2 text-sm text-text-grey">First Name</label>
                        <input
                            value={firstName}
                            onChange={(e) => setFirstName(e.target.value)}
                            required
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        />
                    </div>

                    <div>
                        <label className="block mb-2 text-sm text-text-grey">Last Name</label>
                        <input
                            value={lastName}
                            onChange={(e) => setLastName(e.target.value)}
                            required
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        />
                    </div>

                    <div>
                        <label className="block mb-2 text-sm text-text-grey">Role</label>
                        <select
                            value={role}
                            onChange={(e) => setRole(e.target.value)}
                            required
                            className="w-full px-4 py-3 rounded-md text-white border border-white/10 bg-textfield-grey focus:outline-white hover:border-white/30"
                        >
                            <option value="" disabled>Select role</option>
                            <option value="User">User</option>
                            <option value="Admin">Admin</option>
                        </select>
                    </div>

                    <div>
                        <button
                            onClick={() => {
                                authClient.register(registerRequestDto, jwt)
                                    .then(r => {
                                        toast.success("User created successfully");
                                        if (r.roleName !== "Admin") {
                                            setUsers([...users, r]);
                                        }
                                        navigate(UserRoute);
                                    })
                                    .catch(() => {
                                        toast.error("Could not create user!");
                                    });
                            }}
                            className="w-full py-3 rounded-md text-[var(--color-background-black)] bg-[--color-button-grey] hover:bg-blue-500 hover:text-white"
                        >
                            Create User
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default RegisterView;
