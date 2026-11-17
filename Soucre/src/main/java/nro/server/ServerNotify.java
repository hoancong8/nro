package nro.server;

import nro.models.player.Player;
import nro.server.io.Message;
import nro.services.Service;

import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author 💖 Obito - Đâu Phải Tuấn 💖
 * @copyright 💖 GirlkuN 💖
 *
 */
public class ServerNotify extends Thread {

    public static class NotifyItem {
        public String normalText;
        public String adminText;

        public NotifyItem(String normalText, String adminText) {
            this.normalText = normalText;
            this.adminText = adminText != null ? adminText : normalText;
        }
    }

    private final List<NotifyItem> notifies;

    private static ServerNotify i;

    private ServerNotify() {
        this.notifies = new ArrayList<>();
        this.start();
    }

    public static ServerNotify gI() {
        if (i == null) {
            i = new ServerNotify();
        }
        return i;
    }

    @Override
    public void run() {
        while (!Maintenance.isRuning) {
            try {
                while (!notifies.isEmpty()) {
                    sendThongBaoBenDuoi(notifies.remove(0));
                }
            } catch (Exception e) {

            }
            try {
                Thread.sleep(100);
            } catch (InterruptedException ex) {
            }
        }
    }

    private void sendThongBaoBenDuoi(NotifyItem item) {
        try {
            List<Player> players = Client.gI().getPlayers();
            for (Player pl : players) {
                if (pl != null && pl.getSession() != null) {
                    boolean isAdmin = pl.isAdmin();
                    String text = isAdmin ? item.adminText : item.normalText;
                    try {
                        Message msg = new Message(93);
                        msg.writer().writeUTF(text);
                        pl.sendMessage(msg);
                        msg.cleanup();
                    } catch (Exception e) {
                    }
                    if (isAdmin && !item.adminText.equals(item.normalText)) {
                        Service.getInstance().sendThongBao(pl, item.adminText);
                    }
                }
            }
        } catch (Exception e) {
        }
    }

    public void notify(String text) {
        this.notifies.add(new NotifyItem(text, text));
    }

    public void notify(String normalText, String adminText) {
        this.notifies.add(new NotifyItem(normalText, adminText));
    }
}
